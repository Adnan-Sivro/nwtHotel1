
var cities = [
    {
        city: 'Hotel',
        desc: 'This is the best hote in the city!',
        lat: 43.849173,
        long: 18.379773
    }
];

//Angular App Module and Controller
angular.module('HotelNWT', [])
.controller('Map', function ($scope) {

    var mapOptions = {
        zoom: 15,
        center: new google.maps.LatLng(43.849173, 18.379773),
        mapTypeId: google.maps.MapTypeId.TERRAIN
    }

    $scope.map = new google.maps.Map(document.getElementById('map'), mapOptions);

    $scope.markers = [];

    var infoWindow = new google.maps.InfoWindow();

    var createMarker = function (info) {

        var marker = new google.maps.Marker({
            map: $scope.map,
            position: new google.maps.LatLng(info.lat, info.long),
            title: info.city
        });
        marker.content = '<div class="infoWindowContent">' + info.desc + '</div>';



    }

    for (i = 0; i < cities.length; i++) {
        createMarker(cities[i]);
    }

    $scope.openInfoWindow = function (e, selectedMarker) {
        e.preventDefault();
        google.maps.event.trigger(selectedMarker, 'click');
    }

});