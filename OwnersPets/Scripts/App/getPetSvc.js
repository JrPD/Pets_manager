(function () {
	'use strict';

	angular
        .module('OwnersPets')
        .factory('getPetsSvc', getPetsSvc);

	getPetsSvc.$inject = ['$q', 'petsSvc'];

	function getPetsSvc($q, petsSvc) {
		var service = {
			initialize: initialize,
			navigate: navigate,
			pages: [],
			paging: {
				info: {
					totalItems: 0,
					totalPages: 1,
					currentPage: 0
				}
			},
			Id:-1
		};

		return service;

		function initialize() {
			service.paging.info.currentPage = 1;

			var queryArgs = {
				pageNumber: service.paging.info.currentPage,
				id: service.Id
			};


			return petsSvc.query(queryArgs).$promise.then(
                function (result) {
                	var newPage = {
                		number: pageNumber,
                		pets: []
                	};
                	result.items.forEach(function (pet) {
                		newPage.pets.push(pet);
                	});

                	service.pages.push(newPage);
                	service.paging.info.currentPage = 1;
                	service.paging.info.totalPages = result.totalPages;

                	return result.$promise;
                }, function (result) {
                	return $q.reject(result);
                });
		}

		function navigate(pageNumber) {
			var dfd = $q.defer();

			if (pageNumber > service.paging.info.totalPages) {
				return dfd.reject({ error: "page number out of range" });
			}

			if (service.pages[pageNumber]) {
				service.paging.info.currentPage = pageNumber;
				dfd.resolve();
			} else {
				return load(pageNumber);
			}

			return dfd.promise;
		}

		function load(pageNumber) {
			var queryArgs = {
				pageNumber: pageNumber,
				id: service.Id
			};

			return petsSvc.query(queryArgs).$promise.then(
                function (result) {
                	var newPage = {
                		number: service.paging.info.pageNumber,
                		pets: []
                	};
                	result.items.forEach(function (owner) {
                		newPage.pets.push(owner);
                	});

                	service.pages[pageNumber] = newPage;
                	service.paging.info.currentPage = pageNumber;
                	service.paging.info.totalPages = result.totalPages;
                	service.paging.info.totalItems = result.totalCount;

                	return result.$promise;
                }, function (result) {
                	return $q.reject(result);
                });
		}
	}
})();