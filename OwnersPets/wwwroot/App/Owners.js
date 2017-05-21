// controller function
angular
	.module('OwnersPets')
	.controller('OwnersCtrl', ['$scope', '$http', '$location', '$route', '$routeParams','$q', 'getOwnerSvc', OwnerCtrl])
	
function OwnerCtrl($scope, $http, $route, $routeParams, $location, $q, getOwnerSvc) {
	var uri = "/api/owners/";

	$scope.result = "";
	$scope.removeOwner = removeOwner;
	$scope.addOwner = addOwner;
	$scope.pages = getOwnerSvc.pages;
	$scope.info = getOwnerSvc.paging.info;

	$scope.navigate = navigate;

	$scope.status = {
		type: "info",
		message: "ready",
		busy: false
	};

	activate();

	function activate() {
		//if this is the first activation of the controller load the first page
		if (getOwnerSvc.paging.info.currentPage === 0) {
			navigate(1);
        } else {
            // reloadPetCount($scope);
            updateTotal();
        }

    
	}

    //function reloadPetCount($scope) {
    //    var owners = $scope.pages[$scope.info.currentPage].owners;
    //    if (owners != undefined) {
    //        for (var i = 0; i < owners.length; i++) {
    //            $http.get(uri + owners[i].ownerId)
    //                .then(function (response) {
    //                    if (response.status = 201) {
    //                        $scope.pages[$scope.info.currentPage].owners.indexOf(owners[i]).count = response.data.count;
    //                    }
    //                });
    //        }
        
    //    }
    //}

	function navigate(pageNumber) {
		$scope.status.busy = true;
		$scope.status.message = "loading records";

		getOwnerSvc.navigate(pageNumber)
						.then(function () {
							$scope.status.message = "ready";
						}, function (result) {
							$scope.status.message = "something went wrong: " + (result.error || result.statusText);
						})
						['finally'](function () {
							$scope.status.busy = false;
						});
	}

	function removeOwner(owner) {
		var owners = $scope.pages[$scope.info.currentPage].owners;
		var index = owners.indexOf(owner);
		if (index > -1) {
			owners.splice(index, 1);
		}
		removeFromDb(owner, $scope, $http);
	
	}

	function addOwner() {
		if (!this.newOwner) {
			$scope.result = "Too short name";
			return;
		}
		addToDb(this.newOwner, $scope, $http);
	}

	function addToDb(ownerName, $scope, $http) {
		var owner = {
			ownerName: ownerName,
		}
		$http.post(uri, owner)
		.then(function (response) {
			if (response.status = 201) {
				//$scope.owners.unshift(response.data)
				$scope.result = "Created new owner with name: " + response.data.ownerName;
				updateTotal();
			}
		});
	}

	function removeFromDb(owner, $scope, $http) {
		$http({
			method: 'DELETE',
			url: uri  + owner.ownerId
		})
		.then(function (response) {
			var name = response.data.ownerName;
			$scope.result = "Owner with name: " + name + " removed"
			updateTotal();
		});
	}

	function updateTotal() {
		// load new information in page - replace delete item
		// todo replace pages, with graater number than currentPage
		$scope.pages[$scope.info.currentPage] = null;
		navigate($scope.info.currentPage)
	}

	
}
