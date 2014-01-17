

theApp.controller('BrokersCtrl', function ($scope, $resource, $routeParams, $http) {

    // cheezy way to create the service...
    var Brokers = $resource('../API/Brokers', {}, { 'lookups': { method: 'GET', isArray: true, cache: true } });
    
    /// List of all brokers (lookups)
    $scope.list = Brokers.lookups();

    ///
    $scope.searchText = "";

    /// Specific broker
    $scope.brokerId = $routeParams['brokerId']
    $scope.broker = {};

    if ($scope.brokerId) {
        $http.get('../API/Brokers/' + $scope.brokerId)
            .then(function (resp) {
                $scope.broker = resp.data;
            });
    }

})