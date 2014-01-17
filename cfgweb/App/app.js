var theApp = angular.module('cfgApp', ['ngRoute', 'partials', 'utils'])
.config(function ($routeProvider) {
    $routeProvider

        .when('/', {
            controller: 'MainCtrl',
            templateUrl: 'app/main.html'
        })

        .when('/brokers', {
            controller: 'BrokersCtrl',
            templateUrl: 'app/brokers.html'
        })

        .when('/groups', {
            controller: 'GroupsCtrl',
            templateUrl: 'app/groups.html'
        })

        .when('/reports', {
            controller: 'ReportsCtrl',
            templateUrl: 'app/reports.html'
        })

        .otherwise({
            redirectTo: '/'
        });
})