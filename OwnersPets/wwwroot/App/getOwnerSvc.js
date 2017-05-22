(function () {
	'use strict';

	angular
        .module('OwnersPets')
        .factory('getOwnerSvc', getOwnerSvc);

	getOwnerSvc.$inject = ['$q', 'ownersSvc'];

	function getOwnerSvc($q, ownersSvc) {
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
        	}
        };

		return service;

		function initialize() {
			var queryArgs = {
                pageNumber: service.paging.info.currentPage,
                controller: "owners"
			};

			service.paging.info.currentPage = 1;

			return ownersSvc.query(queryArgs).$promise.then(
                function (result) {
                	var newPage = {
                		number: pageNumber,
                		owners: []
                	};
                	result.items.forEach(function (owner) {
                		newPage.owners.push(owner);
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
			return load(pageNumber);
		}

		function load(pageNumber) {
			var queryArgs = {
                pageNumber: pageNumber,
                controller: "owners"
			};

			return ownersSvc.query(queryArgs).$promise.then(
                function (result) {
                	var newPage = {
                		number: service.paging.info.pageNumber,
                		owners: []
                	};
                	result.items.forEach(function (owner) {
                		newPage.owners.push(owner);
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