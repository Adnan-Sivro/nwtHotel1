﻿angular.module('HotelNWT') 
           .controller('Login3Controller', function ($scope, LoginService) {
               $scope.IsLogedIn = false;
               $scope.Message = '';
               $scope.Submitted = false;
               $scope.IsFormValid = false;

               $scope.LoginData = {
                   Username: '',
                   Password: ''
               };

               
               $scope.$watch('f1.$valid', function (newVal) {
                   $scope.IsFormValid = newVal;
               });

               $scope.Login = function () {

                   $scope.Submitted = true;
                   if ($scope.IsFormValid) {
                       var user = LoginService.GetUser($scope.LoginData);
                        user.then(function (d) {
                           alert(d);
                           if (d.Username != null) {
                               $scope.IsLogedIn = true;
                               $scope.Message = "Successfully login done. Welcome " + d.Username;

                           }
                           else {
                               alert('Invalid Credential!');
                           }
                       });
                   }
               };

           })
           .factory('LoginService', function ($http, $q) {
               var fac = {};
               
               fac.GetUser = function (d) {
                   var defer = $q.defer();
                   var test=  $http({
                       url: '/Data/UserLogin',
                       method: 'POST',
                       data: JSON.stringify(d),
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

                   //return test;
               };
               return fac;
           });

