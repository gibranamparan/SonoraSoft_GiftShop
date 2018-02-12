angular.module('users', [])
.component("userLogin", {
    templateUrl: "Scripts/app/UsersModule/UsersLogin.template.html",
    controller: function ($scope, $http, $window) {        

        $scope.login = function(){
            $http.get('/api/auth').then((res) => { //Success
                console.log(res)
            },//Error
            (res, stats, conf, txt) => { console.log(res) })
        }
    }
})