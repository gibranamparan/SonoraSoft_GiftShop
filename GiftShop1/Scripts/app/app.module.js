'use strict'
var app = angular.module('app', [
    //External
    'LocalStorageModule',
    'ngRoute',
    'smart-table',
    'ui.bootstrap',

    //Internal
    'users',
    'products',
])

app.config(function($locationProvider, $routeProvider, localStorageServiceProvider){
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
    .setPrefix('GiftShop')
    .setStorageType('sessionStorage')
    .setNotify(true, true)
})
