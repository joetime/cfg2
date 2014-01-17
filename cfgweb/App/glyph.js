angular.module('utils', [])
.directive('glyph', function () {
    return {
        restrict: 'E',
        transclude: false,
        template: '<span class="glyphicon glyphicon-{{icon}}"></span>',
        replace: true,
        scope: {
            icon: '@'
        }
    };
});