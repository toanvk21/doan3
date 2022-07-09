var app = angular.module("Home", ['angularUtils.directives.dirPagination']);
app.controller("HomeController", function ($scope, $http, $rootScope, $window) {
    debugger;
    //lấy danh sách danh mục
    $scope.GetAllCategory = function () {
        $http({
            method: "get",
            url: "/Home/GetALLCategory/",
        }).then(function (response) { //trả về dữ liệu lấy được từ controller
            $scope.CategoryList = response.data;
        })
    }
    //lấy danh sách sản phẩm mới
    $scope.GetNewProduct = function () {
        $http({
            method: "get",
            url: "/Home/GetNewProduct/",
        }).then(function (response) {
            $scope.ProductList = response.data;
        })
    }
    //lấy danh sách bán chạy
    $scope.GetTopSelling = function () {
        $http({
            method: "get",
            url: "/Home/GetTopSelling/",
        }).then(function (response) {
            $scope.ProductList = response.data;
        })
    }
    //lấy danh sách sản phẩm slide 
    $scope.GetProductCarousel = function () {
        $http({
            method: "get",
            url: "/Home/GetTopSelling/",
        }).then(function (response) {
            $scope.ProductSelling = response.data;
        })
        $http({
            method: "get",
            url: "/Home/GetHotDeal/",
        }).then(function (response) {
            $scope.ProductHotDeal = response.data;
        })
        $http({
            method: "get",
            url: "/Home/GetOnTop/",
        }).then(function (response) {
            $scope.ProductOnTop = response.data;
        })
    }
    //lấy toàn bộ danh sách sản phẩm
    $scope.GetAllProduct = function () {
        $http({
            method: "get",
            url: "/Home/GetAllProduct/",
        }).then(function (response) {
            $scope.ProductList = response.data;
        })
    }
    //lấy danh sách tin tức
    $scope.GetAllNews = function () {
        $http({
            method: "get",
            url: "/Home/GetAllNews/",
        }).then(function (response) {
            $scope.NewsList = response.data;
        })
    }
    //lấy tên meta của sản phẩm
    $scope.GetMetaProduct = function (MetaName) {
        var item = MetaName;
        localStorage.setItem("metaname", item);
    }
    //lấy nội dung tìm kiếm (loại sp)
    $scope.GetSearch = function () {
        var filter = $scope.selectCategory;
        var search = $scope.txtSearch;
        localStorage.setItem("filter", filter);
        localStorage.setItem("search", search);
    }
    //trả về danh sách tìm kiếm
    $scope.SearchProduct = function () {
        var filter = localStorage.getItem("filter");
        var search = localStorage.getItem("search");
        $http({
            method: "get",
            url: "/Home/Search/?filter=" + filter + "&search=" + search,
        }).then(function (response) {
            $scope.SearchListProduct = response.data;
        })
    }
    //lấy sản phẩm theo danh mục
    $scope.GetProductByCategory = function () {
        $http({
            method: "get",
            url: "/Home/GetProductByCategory/?MetaName=" + localStorage.getItem('metaname')
        }).then(function (response) {

            $scope.ProductListByCategory = response.data;
        })
    }

    //đổ dữ liệu vào trang chi tiết phẩm
    $rootScope.Product = {};
    $scope.LoadProduct = function () {
        $rootScope.Product.Description = localStorage.getItem("Description");
        $scope._ID = localStorage.getItem("ID");
        $scope._Name = localStorage.getItem("Name");
        $scope._MetaName = localStorage.getItem("MetaName");
        $scope._Cost = parseInt(localStorage.getItem("Cost"));
        $scope._Image = localStorage.getItem("Image");
        $scope._Description = localStorage.getItem("Description");
        $scope._Details = localStorage.getItem("Details");
        $scope._HotDeal = localStorage.getItem("HotDeal");
        $scope._IsTopSeller = localStorage.getItem("IsTopSeller");
        $scope._IsOnTop = localStorage.getItem("IsOnTop");
        $scope._IsNew = localStorage.getItem("IsNew");
        $scope._IsStatus = localStorage.getItem("IsStatus");
        $scope._Category = localStorage.getItem("Category");
        $scope._Brand = localStorage.getItem("Brand");
        $http({
            method: "post",
            url: "/Home/GetImageProduct/?id=" + localStorage.getItem("ID"),
        }).then(function (response) {
            $scope.ImageProductList = response.data;
        })
        $http({
            method: "post",
            url: "/Home/GetRelatedProduct/?category=" + localStorage.getItem("Category"),
        }).then(function (response) {
            $scope.ProductList = response.data
        })

    }
    //lấy danh sách bình luận sản phẩm
    $scope.GetReviewProductList = function () {
        $http({
            method: "post",
            url: "/Home/GetReviewProduct/?product=" + localStorage.getItem("ID"),
        }).then(function (response) {
            $scope.ReviewProductList = response.data;
        })
    }
    //thêm bình luận sản phẩm
    $scope.InsertReivewProduct = function () {
        $scope.ReviewProduct = {};
        $scope.ReviewProduct.Product = localStorage.getItem("ID");
        $scope.ReviewProduct.Name = $scope._NamePerson;
        $scope.ReviewProduct.Email = $scope._EmailPerson;
        $scope.ReviewProduct.Content = $scope._ContentPerson;
        var today = new Date();
        var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
        var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
        $scope.ReviewProduct.Date = date + ' ' + time;
        $http({
            method: "post",
            url: "/Home/ReviewProduct/",
            dataType: "json",
            data: JSON.stringify($scope.ReviewProduct)
        }).then(function (response) {
            alert(response.data);
            $scope.GetReviewProductList();
            $scope._NamePerson = "";
            $scope._EmailPerson = "";
            $scope._ContentPerson = "";
        })
    }

    //lấy sản phẩm thông qua id
    $scope.GetProduct = function (id) {
        $http({
            method: "post",
            url: "/Home/GetProduct/?id=" + id,
        }).then(function (response) {
            localStorage.setItem("ID", response.data.ID);
            localStorage.setItem("Name", response.data.Name);
            localStorage.setItem("MetaName", response.data.MetaName);
            localStorage.setItem("Cost", response.data.Cost);
            localStorage.setItem("Image", response.data.Image);
            localStorage.setItem("Description", response.data.Description);
            localStorage.setItem("Details", response.data.Details);
            localStorage.setItem("HotDeal", response.data.HotDeal);
            localStorage.setItem("IsTopSeller", response.data.IsTopSeller);
            localStorage.setItem("IsOnTop", response.data.IsOnTop);
            localStorage.setItem("IsNew", response.data.IsNew);
            localStorage.setItem("IsStatus", response.data.IsStatus);
            localStorage.setItem("Category", response.data.Category);
            localStorage.setItem("Brand", response.data.Brand);
        })
    }
    //lấy danh sách giỏ hàng
    $scope.GetListCart = function () {
        $http({
            method: "post",
            url: "/Home/GetYourCarts/",
        }).then(function (response) {
            $scope.CartList = response.data

        })

    }
    //lấy tổng tiền trong giỏ hàng
    $scope.GetSubTotal = function () {
        $http({
            method: "post",
            url: "/Home/SubTotal/",
        }).then(function (res) {
            $scope.subTotal = res.data
        })
    }
    //thêm sản phẩm vào giỏ hàng
    $scope.AddCart = function (IDProduct) {
        $.ajax({
            url: "/Home/AddCarts/?IDProduct=" + IDProduct,
            contentType: 'json',
            type: "POST",
            success: function () {
                window.location.reload('@Url.Action("YourCart","Home")')
                $scope.GetListCart();
                $scope.GetSubTotal();
            }
        })
    }
    //xóa sản phẩm khỏi giỏ hàng
    $scope.DeleteCart = function (index) {
        $http({
            method: "post",
            url: "/Home/DeleteCart/?index=" + index,
            dataType: "json",
        }).then(function () {
            $scope.GetListCart();
            $scope.GetSubTotal();
        })
    }
    //cập nhật giỏ hàng
    $scope.UpdateCart = function (index, quantity) {
        $http({
            method: "post",
            url: "/Home/UpdateCart/?index=" + index + "&quantity=" + quantity,
            dataType: "json",
        }).then(function () {
            $scope.GetListCart();
            $scope.GetSubTotal();
        })
    }
    //thêm hóa đơn
    $scope.InsertCheckout = function () {
        $scope.CheckOut = {};
        $scope.CheckOut.FirstName = $scope._FirstName;
        $scope.CheckOut.LastName = $scope._LastName;
        $scope.CheckOut.Email = $scope._Email;
        $scope.CheckOut.Address = $scope._Address;
        $scope.CheckOut.PhoneNumber = $scope._PhoneNumber;
        $scope.CheckOut.Note = $scope._Note;
        var today = new Date();
        var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
        var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
        $scope.CheckOut.Date = date + ' ' + time;
        $http({
            method: "post",
            url: "/Home/AddCheckout/",
            dataType: "json",
            data: JSON.stringify($scope.CheckOut)
        }).then(function (res) {
            localStorage.setItem("IDCheckout", res.data);
        })
    }
    //lấy giỏ hàng thông qua id hóa đơn
    $scope.GetCartByID = function (IDCheckOut) {
        if (IDCheckOut == null) {
            IDCheckOut = localStorage.getItem("IDCheckout");
        }
        $http({
            method: "post",
            url: "/Home/GetCartByID/?IDCheckOut=" + IDCheckOut,
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
            url: "/Home/GetCheckOutByID/?IDCheckOut=" + IDCheckOut,
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
    //lấy danh sách tin tức
    $rootScope.News = {};
    $scope.LoadNews = function () {
        var Titile = localStorage.getItem("TitileNews");
        $scope._Titile = Titile;
    }

    //lấy tin tức thông qua id
    $scope.GetNews = function (id) {
        $http({
            method: "post",
            url: "/Home/GetNews/?id=" + id,
        }).then(function (response) {
            localStorage.setItem("IDNews", response.data.ID);
            localStorage.setItem("TitileNews", response.data.Titile);
            localStorage.setItem("ImageNews", response.data.Image);
            localStorage.setItem("ContentNews", response.data.Content);

        })
    }
    //lấy danh sách bình tức bình luân trong tin tức
    $scope.GetCommentNewsList = function () {
        $http({
            method: "post",
            url: "/Home/GetCommentNews/?news=" + localStorage.getItem("IDNews"),
        }).then(function (response) {
            $scope.CommentNewsList = response.data;
        })
    }
    //thêm bình luận
    $scope.InsertCommentNews = function () {
        $scope.CommentNews = {};
        $scope.CommentNews.News = localStorage.getItem("IDNews");
        $scope.CommentNews.Name = $scope._NamePerson;
        $scope.CommentNews.Email = $scope._EmailPerson;
        $scope.CommentNews.Content = $scope._ContentPerson;
        var today = new Date();
        var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
        var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
        $scope.CommentNews.Date = date + ' ' + time;
        $http({
            method: "post",
            url: "/Home/CommentNews/",
            dataType: "json",
            data: JSON.stringify($scope.CommentNews)
        }).then(function (response) {
            alert(response.data);
            $scope.GetCommentNewsList();
            $scope._NamePerson = "";
            $scope._EmailPerson = "";
            $scope._ContentPerson = "";
        })
    }


    //Tracking
    //lấy danh sách tìm kiếm đơn hàng
    $scope.GetAllTracking = function (searchString) {
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
    //lấy id của hóa đơn
    $scope.LoadTracking = function (id) {
        localStorage.setItem("IDCheckOut", id)
        alert(id)
    }
    //lấy hóa đơn thông qua id
    $scope.GetTrackingByID = function (IDCheckOut) {
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
    //đăng nhập
    $scope.Login = function () {
        $scope.Account = {};
        $scope.Account.Username = $scope._Username;
        $scope.Account.Password = $scope._Password;
        $http({
            method: "post",
            url: "/Home/LoginAdmin/",
            dataType: "json",
            data: JSON.stringify($scope.Account)
        }).then(function (res) {
            var item = res.data;
            if (item.toString() == 'true') {
                $window.location.href = '/Admin/Product';
                $scope._Username = "";
                $scope._Password = "";
            } else {
                alert('Tài khoản hoặc mật khẩu không chính xác');
            }
        })
    }
})