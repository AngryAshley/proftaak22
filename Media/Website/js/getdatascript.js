var predefined_val = null;
var cam_alerts = [];
var counter = 0;

$(document).ready(function () {
	setInterval(function(){
        $.ajax({
            type:"GET",
			dataType: "json",
            url:"./includes/get_data.php", //put relative url here, script which will return php

            success:function(response){
                var data = response; // response data from your php script

                if(JSON.stringify(predefined_val) != JSON.stringify(data)){
                    // window.location.href=window.location.href;
					//Get the template
					var template = $("#all-data-template").html();
					//Render output with Mustache.js
					var renderTemplate = Mustache.render(template, data);
					//Append the data to the body
					$("#log").append(renderTemplate);
					
					// camAlert = data;
					predefined_val = data;

					// store alerts for check
					data.forEach(element => {
						if(element['alert_checked'] == false) {
							cam_alerts[counter] = element;
							counter++;
						}
					});

					camAlerts(cam_alerts);
				
                } 
				
				if (predefined_val == null)
				{
					predefined_val = data;
				}
            }
        });                     
    },1000);// function will run every 1 seconds
});

function updateData(camId) {

	console.log("Update Data met ID: " + camId);

	$.ajax({ 
		type: "POST",
		url: "includes/update_data.php",
		data:{
			cam_id : camId,
		},
		success: function (response) {
			console.log("Alert Checked!");
		},
		error: function (response, error) {
			console.log('Error', response, error);
		}
	});
}

