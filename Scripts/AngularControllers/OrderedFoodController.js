angular.module('HotelNWT')  //extending angular module from first part
.controller('OrderedFoodController', function ($scope, OrderedFoodService) { //explained about controller in Part2
    $scope.OrderedFood = [];

    OrderedFoodService.GetOrderedFood().then(function (d) {
        //for (var i in d.data) {
        //    d.data[i].to_date = new Date(parseInt(d.data[i].to_date.substr(6)));
        //    d.data[i].from_date = new Date(parseInt(d.data[i].from_date.substr(6)));
        //}


        //d.data[1].to_date = new Date(parseInt(d.data[1].to_date.substr(6)));
        $scope.OrderedFood = d.data;
    });
})
.factory('OrderedFoodService', function ($http) {
    var fac = {};
    fac.GetOrderedFood = function () {
        return $http.get('/Data/OrderedFood');
    }
    return fac;
});