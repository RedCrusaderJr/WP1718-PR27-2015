function loadHomepage() {
    $.get("api/users/get", { senderID: localStorage.getItem("logged") }, "json")
        .done(function (data) {
            //welcoming bar : data.Username
        })
        .fail(function (jqXHR) {
            if (jqXHR.status === 401 || jqXHR.status === 406) {
                localStorage.removeItem("logged");
                location.reload();
            }
        });
}


function loginHandler() {
    $.post("api/users/login", $("form#login-form").serialize(), "json")
        .done(function (data) {
            $("a#registrate").hide();
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