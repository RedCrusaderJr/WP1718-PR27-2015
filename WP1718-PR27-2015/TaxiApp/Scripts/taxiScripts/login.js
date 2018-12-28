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
                                //alert(jqXHR.statusText);
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

        $("div#reg-div").show();
        $("div#reg-div").load("./Content/partials/login.html"); 

        $("a#register-a").show();
        $("a#register-a").bind("click", registerAction());
    } catch (e) {
        $("div#err-div").show();
        $("div#err-div").append(e.Message);
    }
}

function registerAction() {
    $("a#register-a").hide();
    $("div#home-div").hide();
    $("div#comment-div").hide();
    $("div#err-div").hide();

    $("div#reg-div").show();
    $("div#reg-div").load("./Content/partials/registerCustomer.html");

    $("a#login-a").show();
    $("a#login-a").bind("click", loadLogin());

    return false;
}


//HANDLERS

function loginHandler() {
    $.post("api/users/login", $("form#login-form").serialize(), "json")
        .done(function (data, status, xhr) {
            $("a#login-a").hide();
            $("a#register-a").hide();
            $("div#reg-div").hide();
            localStorage.setItem("logged", data.Username);
            $("div#err-div").hide();
            loadHomepage();
        })
        .fail(function (jqXHR) {
            $("div#err-div").text(jqXHR.responseJSON).show();
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
    $("form#login-form").validate({
        rules: {
            Username: {
                required: true,
                minlength: 3
            },
            Password: {
                required: true,
                minlength: 3
            }
        },
        message: {
            username: {
                required: "Morate uneti ovo polje",
                minlength: "Korisnicko ime mora biti minimum 3 slova dugacak"
            },
            password: {
                required: "Morate uneti ovo polje",
                minlength: "Lozinka mora biti minimum 3 slova dugacak"
            }
        },
        submitHandler: function (form) {
            loginHandler();
        }
    });
}

function validateCustomerRegistration() {
    $("form#reg-customer-form").validate({
        rules: {
            Username: {
                required: true,
                minlength: 3
            },
            Password: {
                required: true,
                minlength: 3
            },
            FirstName: {
                required: false,
                maxlength: 15
            },
            LastName: {
                required: false,
                maxlength: 15
            },
            JMBG: {
                required: false,
            },
            Phone: {
                required: false,
            },
            Email: {
                required: false,
                maxlength: 15
            },
            Gender: {
                required: true,
            },
        },
        message: {
            username: {
                required: "Morate uneti ovo polje",
                minlength: "Korisnicko ime mora biti minimum 3 slova dugacak"
            },
            password: {
                required: "Morate uneti ovo polje",
                minlength: "Lozinka mora biti minimum 3 slova dugacak"
            }
        },
        submitHandler: function (form) {
            customerRegistrationHandler();
        }
    }); 
}

function validateDriverRegistration() {
    $("form#reg-driver-form").validate({
        submitHandler: function (form) {
            driverRegistrationHandler();
        }
    }); 
}

function validateNonDriverProfileUpdate() {
    $("form#update-non-driver-form").validate({
        submitHandler: function (form) {
            nonDriverProfileUpdateHandler();
        }
    });
}

function validateDriverProfileUpdate() {
    $("form#update-driver-form").validate({
        submitHandler: function (form) {
            driverProfileUpdateHandler();
        }
    });
    
}