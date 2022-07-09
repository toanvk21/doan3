var app = angular.module("Order", ['angularUtils.directives.dirPagination']);
app.controller("OrderController", function ($scope, $rootScope, $http) {
    debugger;
    $scope.GetAllOrder = function (searchString) {
        searchString = document.getElementById("txtSearch").value.toString();
        $http({
            method: "get",
            url: "/Admin/Order/GetAllCheckOut/?searchString=" + searchString,
        }).then(function (response) {
            $scope.OrderList = response.data;
        }, function () {
            alert("Lỗi lấy dữ liệu");
        })
    }
    $scope.LoadOrder = function (id) {
        localStorage.setItem("IDCheckOut", id)
        alert(id)
    }
    $scope.GetCartByID = function (IDCheckOut) {
        if (IDCheckOut == null) {
            IDCheckOut = localStorage.getItem("IDCheckout");
        }
        $http({
            method: "post",
            url: "/Admin/Order/GetCartByID/?IDCheckOut=" + IDCheckOut,
        }).then(function (res) {
            $scope.ListCart = res.data;
            var sum = 0;
            for (let i = 0; i < $scope.ListCart.length; i++) {
                sum += $scope.ListCart[i].Cost;
            }
            $scope.subTotal = sum;
        })
        $http({
            method: "post",
            url: "/Admin/Order/GetCheckOutByID/?IDCheckOut=" + IDCheckOut,
        }).then(function (res) {
            $scope.CheckOut = res.data;
            $scope._ID = $scope.CheckOut.ID;
            $scope._FirstName = $scope.CheckOut.FirstName;
            $scope._LastName = $scope.CheckOut.LastName;
            $scope._Email = $scope.CheckOut.Email;
            $scope._Address = $scope.CheckOut.Address;
            $scope._PhoneNumber = $scope.CheckOut.PhoneNumber;
            $scope._Note = $scope.CheckOut.Note;
        })
    }
})