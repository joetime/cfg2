theApp.controller('ImportCtrl', function ($scope /*, $resource, $routeParams, $http*/) {
    $scope.debug = false;
    var logMe = function (obj) {
        if (typeof obj === 'string')
            console.log("[importCtrl.js] " + obj)
        else {
            console.log("[importCtrl.js] >>")
            console.log(obj)
        }
    }

    // DATA 
    $scope.month = 2;
    $scope.year = 2014;
    $scope.provider = "";

    // UI 
    $scope.progress = 0;
    $scope.working = false;
    $scope.complete = false;
    $scope.error = false;
    $scope.errorMessage = "";
    

    // RESPONSE
    $scope.resp = {
        totalDollars: 0,
        lineItems: 0,
        totalCommission: 0
    }

    $scope.OnUploadError = function (err) {

        logMe("error:");
        logMe(err)

        $scope.working = false;
        $scope.error = true;

        $scope.errorMessage = err.StatusText;

        // trigger a refresh
        $scope.$apply();
    };

    $scope.OnUploadComplete = function (resp) {
        logMe("compelteFn:")
        logMe(resp);

        $scope.complete = true;
        $scope.working = false;
        $scope.error = false;

        $scope.resp.totalCommission = 100;
        $scope.resp.totalDollars = 10000;
        $scope.resp.lineItems = 5021;

        // trigger a refresh
        $scope.$apply();
    };

    $scope.upload_click = function () {

        // validate
        if ($("#fileIn").val() == "") {
            alert("Please choose a file.")
            return false;
        }

        $scope.working = true;
        $scope.complete = false;

        var file = document.getElementById("fileIn");

        /* Create a FormData instance */
        var formData = new FormData();
        /* Add the file */
        formData.append("upload", file.files[0]);
        // other form data
        formData.append("month", $scope.month);
        formData.append("year", $scope.year);
        formData.append("provider", $scope.provider);

        // send! // data, completeFn, progressFn, ErrorFn 
        FileUpload(
            formData, $scope.OnUploadComplete, function (resp) {
                logMe("progress:");
                logMe(resp)

                $scope.progress = resp;
            },
            $scope.OnUploadError);
    }




    //$(document).ready(function () { });
});