var instagram = angular.module('instagram', []);

instagram.factory('instaPhotos', ['$http', function ($http) {

    return {
        getPhotos: function (url, callback) {
            $http.jsonp(url).
            success(function (data, status) {
                callback(data);
            });
        }
    }
}]);

instagram.controller('InstagramCtrl', ['$scope', 'instaPhotos', function ($scope, instaPhotos) {
    $scope.endpoint = "https://api.instagram.com/v1/users/19483745/media/recent/?client_id=0e1a378bb152469d8be0749a4a02bbd3&callback=JSON_CALLBACK";
    $scope.url = $scope.endpoint;
    $scope.data = null;
    $scope.photos = [];
    $scope.isLoading = false;
    $scope.moreToLoad = true;

    $scope.loadPhotos = function () {
        if (!$scope.isLoading && $scope.moreToLoad) {
            $scope.isLoading = true;
            instaPhotos.getPhotos($scope.url, function (data) {
                $scope.isLoading = false;
                $scope.data = data;
                $scope.photos.push.apply($scope.photos, data.data);
                $scope.url = $scope.endpoint + "&max_id=" + data.pagination.next_max_id;
                $scope.moreToLoad = data.data.length > 0;
            });
        }
    };

    $scope.loadPhotos();
}]);

instagram.directive('whenScrolled', ['$window', function ($window) {
    return {
        link:function (scope, element, attrs) {
            var e = element[0];
            var $myWindow = angular.element($window);

            $myWindow.bind('scroll', function () {
                var elementHeight = e.offsetHeight;
                var scrollAmount = ($window.innerHeight / 2) + $window.scrollY;
                var delta = 10;

                if (elementHeight - (scrollAmount - delta) < 0) {
                    scope.$apply(attrs.whenScrolled);
                }
            });
        }
    };
}]);

// instagram.directive('fancybox', function () {
//     return {
//         restrict: 'A',
//         link: function (scope, element, attrs) {
//             $(element).fancybox();
//         }
//     }
// });

// instagram.directive('fancybox', function($compile, $timeout){
//     return {
//         link: function($scope, element, attrs) {
//             element.fancybox({
//                 hideOnOverlayClick:false,
//                 hideOnContentClick:false,
//                 enableEscapeButton:false,
//                 showNavArrows:false,
//                 onComplete: function(){
//                     $timeout(function(){
//                         $compile($("#fancybox-content"))($scope);
//                         $scope.$apply();
//                         $.fancybox.resize();
//                     })
//                 }
//             });
//         }
//     }
// });