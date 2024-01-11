
                         var contactUrl = "@Url.Action("SubmitContactForm", "Contact")";

    SubmitForm = (event) => {
        event.preventDefault();
        alert("11")
        let param = {
            Name: $("#Name").val(),
            Email: $("#email").val(),
            Phone: $("#Phone").val(),
            Message: $("#Message").val(),
        };

        console.log("params", param);
        if ($("#data-form-contact").valid()) {
            $.ajax({
                url: contactUrl,
                dataType: "json",
                type: "POST",
                data: param,
                success: (result) => {
                    console.log(result, "rs");
                },
                error: (xhr) => {
                    alert("Something went error! Pls try again!");
                }
            })
        }