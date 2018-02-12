'use strict'
angular.module('app', [
    //External
    'ngRoute',
    'ui.bootstrap',
    'LocalStorageModule',

    //Internal
    'users',
    'products',
])
.config(function($locationProvider, $routeProvider, localStorageServiceProvider){
    //Angular Router Config
    $routeProvider.
    when("/",{
        template:"<products-list></products-list>"
    }).
    when("/cart/",{
        template:"<cart-summary></cart-summary>"
    }).
    when("/login/",{
        template:"<user-login></user-login>"
    })

    //Local Storage Config
    localStorageServiceProvider
    .setPrefix('GiftShop');
})