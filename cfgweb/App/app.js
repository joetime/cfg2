var theApp = angular.module('cfgApp', ['ngRoute', 'ngResource', 'partials', 'utils' //, 'Brokers'
])
.config(function ($routeProvider) {
    $routeProvider

        .when('/', {
            controller: 'MainCtrl',
            templateUrl: 'app/main.html'
        })

        .when('/brokers/:brokerId?', {
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