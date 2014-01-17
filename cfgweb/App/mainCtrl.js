theApp.controller('MainCtrl', function($scope, $resource) {

    var Files = $resource('../API/Files', {}, { 'lookups': { method: 'GET', isArray: true, cache: true } });

    $scope.list = Files.lookups();

    $scope.monthName = function (i) {
        return moment().month(i).format('MMMM');
    };

})