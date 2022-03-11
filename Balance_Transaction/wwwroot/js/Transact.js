$(document).ready(function () {

    // Attach Button click event listener 
    $(".myBtn").click(function (e) {

        // show Modal
        $("#myModal").modal('show');
        let personId = $(this).attr("data-personId");
        let nameValue = $(this).attr("data-name");
        let surnameValue = $(this).attr("data-surname");

        document.getElementById("TransId").innerText = nameValue + " " + surnameValue;
        document.getElementById("totrans").value = personId;
    });

    $(".close-btn").click(function () {

        // hide Modal
        $("#myModal").modal('hide');
    });

    $(".fill-btn").click(function (e) {
        //alert("clicked");
        $("#fillmodal").modal('show');
        let personId = $(this).attr("data-personId");
        document.getElementById("tofill").value = personId;
        console.log(personId);
        $(".close-btn").click(function () {
            // hide Modal
            $("#fillmodal").modal('hide');
        });
    })
});
