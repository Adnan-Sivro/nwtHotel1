angular.module('HotelNWT') //extending angular module from First Part
          .controller('RegistrationController', function ($scope, RegistrationService) { //explained about controller in Part 2
              //Default Variable
              $scope.submitText = "Save";
              $scope.submitted = false;
              $scope.message = '';
              $scope.isFormValid = false;
              $scope.User = {
                  Username: '',
                  Password: '',
                  Email: ''
              };
              //Check form Validation // here f1 is our form name
              $scope.$watch('f1.$valid', function (newValue) {
                  $scope.isFormValid = newValue;
              });
              //Save Data
              $scope.SaveData = function (data) {
                  if ($scope.submitText == 'Save') {
                      $scope.submitted = true;
                      $scope.message = '';
                     

                          if ($scope.isFormValid) {
                              $scope.submitText = 'Please Wait...';
                              $scope.User = data;
                              if ($scope.User.Password == $scope.password_verify) {

                                  RegistrationService.registracija($scope.User).then(function (response) {
                                      if (response.success) {
                                          alert('Email za potvrdu registracije je poslan. Provjerite Vaš inbox!');
                                      } else {
                                          $scope.error = response.message;
                                          $scope.errorMessage = $sce.trustAsHtml(response.message);
                                          $scope.dataLoading = false;
                                      }
                                  })


                /*              RegistrationService.SaveFormData($scope.User).then(function (d) {
                                  alert(d);
                                  if (d == 'Success') {
                                      //have to clear form here
                                      ClearForm();
                                  }
                                  $scope.submitText = "Save";
                              });*/
                          }
                          else {
                                  $scope.message = "Different passwords!";

                          }
                          } else {
                              $scope.message = 'Please fill required fields value';

                      }
                  }
              }
              //Clear Form (reset)
              function ClearForm() {
                  $scope.User = {};
                  $scope.f1.$setPristine(); //here f1 our form name
                  $scope.submitted = false;
              }
          })
          .factory('RegistrationService', function ($http, $q) { //explained about factory in Part 2
              //here $q is a angularjs service with help us to run asynchronous function and return result when processing done
              var fac = {};
              fac.SaveFormData = function (data) {
                  var defer = $q.defer();
                  $http({
                      url: '/Data/Register',
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

              fac.registracija = function (data) {
                      var defer = $q.defer();
                      $http({
                          url: '/Data/RegistracijaJson',
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

                  /* $http.post('/Account/RegistracijaJson', { username: username, password: password, Email: email })
                      .success(function (response) {
                          if (response !== true) {
                              callback({ success: false, message: response });
                          } else {
                              var newResponse = { success: true };
                              callback(newResponse);
                          }
                      }).error(function (e) {
                          //Failed Callback
                          alert('Registration failed!');
                      })*/
                  return fac;
              
          });

