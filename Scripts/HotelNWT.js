(function () {
    //Create a Module 
    var app = angular.module('HotelNWT', ['ngRoute']);  // Will use ['ng-Route'] when we will implement routing
    //Create a Controller
    app.controller('HomeController', function ($scope) {  // here $scope is used for share data between view and controller
        $scope.Message = "Yahoooo! we have successfully done our first part.";
    });
})();