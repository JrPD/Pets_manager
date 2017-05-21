angular
	.module('OwnersPets')
	.controller('PetCtrl', ['$scope', '$http', '$routeParams', '$q', 'getPetsSvc', PetCtrl])
function PetCtrl($scope, $http, $routeParams, $q, getPetsSvc) {
	$scope.ownerId = $routeParams.ownerId;
	$scope.removePet = removePet;
	$scope.addPet = addPet;

	$scope.pages = getPetsSvc.pages;
	getPetsSvc.paging.info.currentPage = 1;
	$scope.info = getPetsSvc.paging.info;
	getPetsSvc.Id = $scope.ownerId;

	$scope.navigate = navigate;

	$scope.status = {
		type: "info",
		message: "ready",
		busy: false
	};

	activate();

	var uriOwner = "/api/owners/" + $scope.ownerId
	var uri = "/api/pets/";

	$http.get(uriOwner)
		.then(function (response) {
			var data = response.data;
			$scope.owner = data;
		});

	function addPet() {
		if (this.newPet) {
			addToDb(this.newPet, $scope, $http);
		}
		$scope.result = "Too short name";
	}

	function addToDb(name, $scope, $http) {
		var pet = {
			petName: name,
			ownerId: $scope.owner
		};
		$http.post(uri, pet)
		.then(function (response) {
			if (response.status = 201) {
				$scope.owner.pets.unshift(response.data)
				$scope.result = "Created new pet with name: " + response.data.petName;
				updateTotal();
			}
		});
	}

	function removePet(pet) {
		var index = $scope.owner.pets.indexOf(pet);
		if (index > -1) {
			$scope.owner.pets.splice(index, 1);
		}
		removeFromDb(pet, $scope, $http);
	}
	function removeFromDb(pet, $scope, $http) {
		$http({
			method: 'DELETE',
			url: uri + pet.petId
		})
		.then(function (response) {
			var name = response.data.petName;
			$scope.result = "Pet with name: " + name + " removed"
			updateTotal();
		});
	}

	function activate() {
		//if this is the first activation of the controller load the first page
		if (getPetsSvc.paging.info.currentPage === 0) {
			navigate(1);
		}
		else {
			updateTotal();
		}
	}

	function navigate(pageNumber) {
		$scope.status.busy = true;
		$scope.status.message = "loading records";

		getPetsSvc.navigate(pageNumber)
						.then(function () {
							$scope.status.message = "ready";
						}, function (result) {
							$scope.status.message = "something went wrong: " + (result.error || result.statusText);
						})
						['finally'](function () {
							$scope.status.busy = false;
						});
	}

	function updateTotal() {
		$scope.pages[$scope.info.currentPage] = null;
		navigate($scope.info.currentPage)
	}
}