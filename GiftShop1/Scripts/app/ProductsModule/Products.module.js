angular.module('products', [])
.component("productsList", {
    templateUrl: "Scripts/app/ProductsModule/ProductsList.template.html",
    controller: function ($scope, $http, $uibModal) {
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
                res=>{ //Clicked a button
                    console.log("Se clickeo boton con opcion:",res)
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
//Modal product details controller
.controller("ModalProductDetailsCtrl",function ($uibModalInstance,prodID) {
    $ctrl = this
    $ctrl.prodID = prodID
    $ctrl.qty = 0

    $ctrl.ok = function () {
        $uibModalInstance.close($ctrl.qty);
    };

    $ctrl.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
})