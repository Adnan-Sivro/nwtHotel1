angular.module('HotelNWT')
        .controller('ReservationController', function ($scope, ReservationService) {
            $scope.submitText = "Save";
            $scope.submitted = false;
            $scope.message = '';
            $scope.isFormValid = true;
            $scope.Reservation = {
                From_Date: '',
                To_Date: '',
                Type: ''
            };
            $scope.$watch('f1.$valid', function (newValue) {
                $scope.isFormValid = newValue;
            });
            $scope.SaveData = function (data) {
                if ($scope.submitText == 'Save') {
                    $scope.submitted = true;
                    $scope.message = '';

                    if ($scope.isFormValid) {
                        $scope.submitText = 'Please Wait...';
                        $scope.Reservation = data;
                        ReservationService.SaveFormData($scope.Reservation).then(function (d) {
                            alert(d);
                            if (d == 'Success') {
                                //have to clear form here
                                ClearForm();
                            }
                            $scope.submitText = "Save";
                        });
                    }
                    else {
                        $scope.message = 'Please fill required fields value';
                    }
                }
            }
            function ClearForm() {
                $scope.User = {};
                $scope.f1.$setPristine(); //here f1 our form name
                $scope.submitted = false;
            }
        })
        .factory('ReservationService', function ($http, $q) { 
            var fac = {};
            fac.SaveFormData = function (data) {
                var defer = $q.defer();
                $http({
                    url: '/Data/Reservation',
                    method: 'POST',
                    data: JSON.stringify(data),
                    headers: { 'content-type': 'application/json' }
                }).success(function (d) {
                    // Success callback
                    defer.resolve(d);
                }).error(function (e) {
                    //Failed Callback
                    alert('Error!');
                    defer.reject(e);
                });
                return defer.promise;
            }
            return fac;
        });
        