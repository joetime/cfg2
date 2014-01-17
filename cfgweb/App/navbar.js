angular.module('partials', [])
.directive('navbar', function() {
    return {
        restrict: 'E',
        transclude: false,
        templateUrl: 'app/navbar.html',
        replace: true,
        scope: {
        },
        controller: function ($scope, $element) {
            
            console.log(window.location.hash);

            $scope.homeActive = function () { return (window.location.hash.length <= 2) };

            $scope.brokersActive = function () { return window.location.hash.indexOf('brokers') > 0 };
            $scope.groupsActive = function () { return window.location.hash.indexOf('groups') > 0 };
            $scope.reportsActive = function () { return window.location.hash.indexOf('reports') > 0 };
        },
    };
})