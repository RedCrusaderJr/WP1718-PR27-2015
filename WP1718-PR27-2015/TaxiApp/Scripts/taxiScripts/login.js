function loadHomepage() {
    try {
        $.get("api/users/get", { senderID: localStorage.getItem("logged") }, "json")
            .done(function (data) {
                $("div#reg-div").hide();
                $("#register-a").hide();

                $("div#home-div").text("Dobrodosli " + data.Username);
                $("div#home-div").show();

                $("a#profile").bind("click", function () {
                    $("div#home-div").hide();

                    $.get("api/users/getHomePage", { senderID: data.Username }, "json")
                        .done(function (data) {
                            $("div#reg-div").load(data);
                            $("div#reg-div").show();
                        })
                        .fail(function (jqHXR) {
                            if (jqHXR.status === 401 || jqHXR.status === 403) {
                                alert(jqXHR.responseJSON);
                                localStorage.removeItem("logged");
                                location.reload();
                            }
                            else {
                                alert(jqXHR.statusText);
                            }
                        });

                    return false;
                });
                $("a#profile").show();

                $("a#logout").bind("click", function (e) {
                    e.preventDefault();
                    $.post("api/users/logout", "=" + localStorage.getItem("logged"))
                        .done(function () {
                            localStorage.removeItem("logged");
                            location.reload();
                            return false;
                        })
                        .fail(function () {
                            if (jqXHR.status === 403) {
                                alert(jqHXR.responseJSON);
                                localStorage.removeItem("logged");
                                location.reload();
                            }
                            else {
                                localStorage.removeItem("logged");
                                location.reload();
                                return false;
                            }
                        });
                });
                $("a#logout").show();

                $("a#add-driver").bind("click", function () {
                    $("div#home-div").hide();

                    $("div#reg-div").load("./Content/partials/registerDriver.html");
                    $("div#reg-div").show();

                    return false;
                });

                if (data.Role === 2) {
                    $("a#add-driver").show();
                }
                else {
                    $("a#add-driver").hide();
                }

                //logged (customer=1 || admin=2) => adding taxi drive

                //logged (driver=3) => list drives taxi drives
            })
            .fail(function (jqXHR) {
                if (jqXHR.status === 401 || jqXHR.status === 406) {
                    localStorage.removeItem("logged");
                    location.reload();
                }
            });
    } catch (e) {
        $("div#err-div").append(e.Message);
        $("div#err-div").show();
    }
}

function loadLogin() {
    try {
        $("a#login-a").hide();
        $("a#profile").hide();
        $("a#add-driver").hide();
        $("a#add-taxi-drive").hide();
        $("a#all-taxi-drivers").hide();
        $("a#created-taxi-drives").hide();
        $("a#logout").hide();

        $("div#home-div").hide();
        $("div#comment-div").hide();
        $("div#err-div").hide();

        $("div#reg-div").load("./Content/partials/login.html");
        $("div#reg-div").show();

        $("a#register-a").bind("click", registerAction());
        $("a#register-a").show();
    } catch (e) {
        $("div#err-div").append(e.Message);
        $("div#err-div").show();
    }
}

function registerAction() {
    $("a#register-a").hide();
    $("div#home-div").hide();
    $("div#comment-div").hide();
    $("div#err-div").hide();

    $("div#reg-div").load("./Content/partials/registerCustomer.html");
    $("div#reg-div").show();

    $("a#login-a").bind("click", loadLogin());
    $("a#login-a").show();

    return false;
}


//HANDLERS

function loginHandler() {
    $.post("api/users/login", $("form#login-form").serialize(), "json")
        .done(function (data) {
            $("a#register-a").hide();
            $("div#reg-div").hide();
            localStorage.setItem("logged", data.Username);
            $("div#err-div").hide();
            loadHomepage();
        })
        .fail(function (jqXHR) {
            $("div#err-div").text(jqXHR.responseJSON["Message"]).show();
        });
}

function customerRegistrationHandler() {
    $.post("api/users/postCustomer", $("form#reg-customer-form").serialize(), "json")
        .done(function (data) {

        })
        .fail({

        });
}

function driverRegistrationHandler() {
    $.post("api/users/postDriver", $("form#reg-driver-form").serialize(), "json")
        .done({

        })
        .fail({

        });
}

function nonDriverProfileUpdateHandler() {
    $.put("api/users/putNonDriver", $("form#update-non-driver-form").serialize(), "json")
        .done({

        })
        .fail({

        });
}

function driverProfileUpdateHandler() {
    $.put("api/users/putDriver", $("form#update-driver-form").serialize(), "json")
        .done({

        })
        .fail({

        });
}


//VALIDATIONS

function validateLogin() {
    loginHandler();
}

function validateCustomerRegistration() {
    customerRegistrationHandler();
}

function validateDriverRegistration() {
    driverRegistrationHandler();
}

function validateNonDriverProfileUpdate() {
    nonDriverProfileUpdateHandler();
}

function validateDriverProfileUpdate() {
    driverProfileUpdateHandler();
}