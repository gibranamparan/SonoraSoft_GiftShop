angular.module('users', [])
//Login screen controller
.component("userLogin", {
    templateUrl: "Scripts/app/UsersModule/UsersLogin.template.html",
    controller: function ($scope, $rootScope, $location, authService) {        
        $scope.loginData = { userName: "", password: "" };
        $scope.message = "";

        $scope.login = function () {
            authService.login($scope.loginData).then(function (response) {
                //If user logged successfully, login link is refreshed to show logout
                $rootScope.$broadcast("refreshLoginInfo")
                $location.path('/');//Redirect to router root
            },
             function (err) {
                 $scope.message = err.error_description;
             });
        };
    
    }
})
//Controller for Login/LogOut link in the menu
.controller("LoginInfoCtrl",function ($scope, $location, localStorageService, authService) {   
    //If there is authorization data en local storage, show LogOut link
    $scope.authData = localStorageService.get('authorizationData')

    $scope.logOut = function(){
        authService.logOut()
        $scope.authData = null;
        $location.path('/login/');//Redirect to router root
    }

    //Listener to check if session las logged out
    $scope.$on('refreshLoginInfo',val=>{
        $scope.authData = localStorageService.get('authorizationData')
    })
})