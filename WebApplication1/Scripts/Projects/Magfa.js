angular.module('GTINReportApp', ['ui.grid', 
    'ngTouch',
    'ui.bootstrap',
    'ui.grid.selection',
    'ui.grid.resizeColumns',
    'ui.grid.autoResize',
    /*'ngPersian'*/])

    .controller('GTINReportCtrl', ['$scope', '$http', function ($scope, $http) {

        $scope.ReportSearchModel = {
            FromDate: null,
            ToDate: null
        }

        $scope.GLNsCount = function () {
            $http({
                method: 'post',
                url: '/api/magfactrl/GLNsCount',
                data: $scope.ReportSearchModel
            }).then(function (data) {
                if (data.data.Succeeded) {
                    $scope.DataResult = data.data.Result;
                }
            }, function (data) {
                $scope.message = data.data.Message;
                console.log($scope.message);
                //mynotify("error", $scope.message)
            });
        };

        $scope.GLNsCount();

    }])

//angular.bootstrap(document.getElementById("GTINReportID"), ['GTINReportApp']);