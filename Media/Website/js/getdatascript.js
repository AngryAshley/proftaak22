var predefined_val = null;

$(document).ready(function () {
	setInterval(function(){
        $.ajax({
            type:"GET",
			dataType: "json",
            url:"./includes/get_data.php", //put relative url here, script which will return php

            success:function(response){
                var data = response; // response data from your php script

				if (predefined_val == null)
				{
					predefined_val = data;
				}

                if(JSON.stringify(predefined_val) != JSON.stringify(data)){
                    window.location.href=window.location.href;
                } 
            }
        });                     
    },1000);// function will run every 1 seconds
});


function getData() {
	// get all the coins from the database
	$.ajax({
		type: "GET",
		dataType: "json",
		url: "./includes/get_data.php",

		success: function (response) {
			predefined_val = response;
		},
		error: function (response, error) {
			console.log('Error', response, error);
		}
	});
}

