'use strict'
angular.module('app', [
    //External
    'ngRoute',
    'ui.bootstrap',

    //Internal
    'products',
])
.config(function($locationProvider, $routeProvider){
    $routeProvider.
    when("/",{
        template:"<products-list></products-list>"
    }).
    when("/cart/",{
        template:"<cart-summary></cart-summary>"
    })
})