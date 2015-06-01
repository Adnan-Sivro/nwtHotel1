angular.module('HotelNWT')  //extending angular module from first part
.controller('ReservatedRoomsController', function ($scope, ReservatedRoomsService) { //explained about controller in Part2
    $scope.ReservatedRooms = [];

    ReservatedRoomsService.GetReservatedRooms().then(function (d) {
        for (var i in d.data){
            d.data[i].to_date = new Date(parseInt(d.data[i].to_date.substr(6)));
            d.data[i].from_date = new Date(parseInt(d.data[i].from_date.substr(6)));
        }

        
        //d.data[1].to_date = new Date(parseInt(d.data[1].to_date.substr(6)));
        $scope.ReservatedRooms = d.data;
    });
})
.factory('ReservatedRoomsService', function ($http) {
    var fac = {};
    fac.GetReservatedRooms = function () {
        return $http.get('/Data/ReservatedRooms');
    }
    return fac;
});