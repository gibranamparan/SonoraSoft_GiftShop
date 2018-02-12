angular.module('products', [])
.component("productsList", {
    templateUrl: "Scripts/app/ProductsModule/ProductsList.template.html",
    controller: function ($scope, $http, $uibModal, localStorageService) {
        $scope.selectedProducts = []
        //Recovering state of selected products by the user
        //if($window.sessionStorage.selectedProducts){
        if(localStorageService.get('selectedProducts')){
            //$scope.selectedProducts = JSON.parse($window.sessionStorage.selectedProducts)
            $scope.selectedProducts = localStorageService.get('selectedProducts')
            console.log($scope.selectedProducts)
        }
        $scope.totalQty = $scope.selectedProducts.map(item=>{return item.qty}).reduce((itemA,itemB)=>{ 
            return itemA + itemB
        },0)

        //Open Modal To see product details
        $scope.openModalProductDetails = function(prodID){
            //Selected product ID is received and passed to modal
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'modalsProductDetails.html',
                controller:'ModalProductDetailsCtrl',
                controllerAs: '$ctrl',
                size: 'lg',
                resolve: {
                    prodID: function(){
                        return prodID
                    }
                }
              });
              modalInstance.result.then(
                res=>{ //Clicked "Add to Cart" button in modal
                    if($scope.selectedProducts){
                        prodFound = $scope.selectedProducts.find(item=>item.prodID == res.prodID)
                        if(prodFound){
                            prodFound.qty = res.qty //Update qty for selected product
                        }else{
                            $scope.selectedProducts.push(res) //Add new selected product
                        }
                        $scope.totalQty = $scope.selectedProducts.map(item=>{return item.qty}).reduce((itemA,itemB)=>{ 
                            return itemA + itemB
                        },0)
                    }
                    
                    console.log($scope.selectedProducts)
                    //$window.sessionStorage.selectedProducts = JSON.stringify($scope.selectedProducts)
                    localStorageService.set('selectedProducts',$scope.selectedProducts)
                },
                res=>{ //Clicked out of the modal
                    console.log("Modal closed")
                },
        )}
        
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

        //Getting the list of all products
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
            //$scope.selectedProducts = JSON.parse($window.sessionStorage.selectedProducts)
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
            console.log($scope.selectedProducts)
        },//Error
        (res, stats, conf, txt) => { console.log(res) })
    }
})
//Modal product details controller
.controller("ModalProductDetailsCtrl",function ($uibModalInstance,prodID) {
    $ctrl = this
    $ctrl.prodID = prodID
    $ctrl.qty = 0

    $ctrl.ok = function () {
        $uibModalInstance.close({prodID : prodID, qty:$ctrl.qty});
    };

    $ctrl.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
})