//https://code.msdn.microsoft.com/AngularJS-with-Web-API-43e5de16/sourcecode?fileId=139277&pathId=1963746534


(function () {
	'use strict';
	var app = angular.module('OwnersPets', ['ngResource', 'ngRoute'])

	app.config(['$routeProvider', '$locationProvider',
		function ($routeProvider, $locationProvider) {
			$routeProvider
			  .when('/Pets/:ownerId', {
			  	templateUrl: '/Pets.html',
			  	controller: 'PetCtrl',
			  	controllerAs: 'PetCtrl'
			  })
			 .when('/', {
			 	templateUrl: 'Owners.html',
			 	controller: 'OwnersCtrl',
			 	controllerAs: 'OwnersCtrl'
			 });

			$locationProvider.html5Mode(true);
		}]);

	app.factory('ownersSvc', function ($resource) {
		return $resource("api/owners/:id",
			{ id: "@id" },
			{                  
				'query': {
					method: 'GET',
					url:'/api/owners/:pageSize/:pageNumber/',
					params: { pageSize: 3, pageNumber: '@pageNumber' }
				} 
			});
	});
	app.factory('petsSvc', function ($resource) {
		return $resource("api/pets/:id",
			{ id: "@id" },
			{
				'query': {
					method: 'GET',
					url: '/api/pets/:pageSize/:pageNumber/',
					params: { pageSize: 3, pageNumber: '@pageNumber' }
				}
			});
	});
})();