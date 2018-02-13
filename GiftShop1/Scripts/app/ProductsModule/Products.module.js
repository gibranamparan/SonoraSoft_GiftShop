angular.module('products', [])
.component("productsList", {
    templateUrl: "Scripts/app/ProductsModule/ProductsList.template.html",
    controller: function ($scope,$rootScope, $http, $uibModal, localStorageService) {
        $scope.selectedProducts = []
        $scope.safeProducts = []
        $scope.products = []

        //Recovering state of selected products by the user
        if(localStorageService.get('selectedProducts')){
            //$scope.selectedProducts = JSON.parse($window.sessionStorage.selectedProducts)
            $scope.selectedProducts = localStorageService.get('selectedProducts')
            console.log($scope.selectedProducts)
        }
        $scope.totalQty = $scope.selectedProducts.map(item=>{return item.qty}).reduce((itemA,itemB)=>{ 
            return itemA + itemB
        },0)

        //Open Modal To see product details
        $scope.openModalProductDetails = (prodID)=>{
            //Selected product ID is received and passed to modal
            $scope.modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'modalsProductDetails.html',
                controller:'ModalProductDetailsCtrl',
                controllerAs: '$ctrl',
                size: 'lg',
                resolve: { prodID: function(){ return prodID } }
              });
              $scope.modalInstance.result.then(
                res=>{ //Clicked "Add to Cart" button in modal
                    //CODIGO DEL EVENTO DE UDOATE DE PRODUCTOS SELECCIONADOS
                },
                res=>{ //Clicked out of the modal
                    console.log("Modal closed")
                }
            )
        }
        
        $scope.clearSelectedProducts = function(){
            $scope.selectedProducts = [];
            $scope.totalQty = 0
            localStorageService.remove('selectedProducts')
        }

        //Evento to update seletected products
        $scope.$on('updateSelectedProducts',(event, res)=>{
            debugger
            var prodFound = $scope.selectedProducts.find(item=>item.prodID == res.prodID)

            if(prodFound)
                prodFound.qty = res.qty //Update qty for selected product
            else
                $scope.selectedProducts.push(res) //Add new selected product
            
            $scope.totalQty = $scope.selectedProducts.map(item=>{return item.qty}).reduce((itemA,itemB)=>{ 
                return itemA + itemB
            },0)
            
            localStorageService.set('selectedProducts',$scope.selectedProducts)
        })
        
        //Getting the list of all products
        $http.get('/api/products').then((res) => { //Success
            var products = res.data.map((item,idx)=> {
                item.qty = 0
                return item
            });
            $scope.products = products
        },//Error
        (res, stats, conf, txt) => { console.log(res) })
    }
})
.component("productDetails", {
    templateUrl: "Scripts/app/ProductsModule/ProductDetails.template.html",
    bindings:{
        prodID: '@',
        qty:'='
    },
    controller: function ($scope, $http) {
        $scope.prodID = $ctrl.prodID
        $scope.product = {}
        console.log("Entro product details con ID",$ctrl.prodID)

        //Getting product details 
        $http.get(`/api/products/${$ctrl.prodID}`).then((res) => { //Success
            console.log("Detalles de producto recibido",res)
            $scope.product = res.data
        },//Error
        (res, stats, conf, txt) => { console.log(res) })
    }
})
.component("cartSummary", {
    templateUrl: "Scripts/app/ProductsModule/Cart.template.html",
    controller: function ($scope, $http, $uibModal, $window, localStorageService) {
        $scope.selectedProducts = []
        $scope.TotalAmount = 0
        //Recovering state of selected products by the user
        if(localStorageService.get('selectedProducts')){
            $scope.selectedProducts = localStorageService.get('selectedProducts')
            console.log($scope.selectedProducts)
        }

        //Getting the list of all products
        $http.get(`/api/products/`).then((res) => { //Success
            console.log("Detalles de producto recibido",res)
            var products = res.data
            $scope.selectedProducts = $scope.selectedProducts.filter(item=>{return item.qty>0}).map(item=>{
                var itemFound = products.find(item2=>{return item2.productID == item.prodID })
                if(itemFound)
                {
                    item.price = itemFound.price
                    item.description = itemFound.description
                    item.totalAmount = item.qty * item.price
                    item.name = itemFound.name
                }
                return item;
            })
            $scope.TotalAmount = $scope.selectedProducts.map(item=>{return item.totalAmount}).reduce((itemA,itemB)=>{ 
                return itemA + itemB
            },0)
        },//Error
        (res, stats, conf, txt) => { console.log(res) })
    }
})
//Modal product details controller
.controller("ModalProductDetailsCtrl",function ($uibModalInstance,$rootScope,prodID) {
    $ctrl = this
    $ctrl.prodID = prodID
    $ctrl.qty = 0

    $ctrl.ok = function () {
        //Calls products list to update the counter of products
        $rootScope.$broadcast("updateSelectedProducts",{prodID : prodID, qty:$ctrl.qty})
        $uibModalInstance.close({prodID : prodID, qty:$ctrl.qty});
    };

    $ctrl.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
})