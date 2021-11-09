$(document).ready(function () {

	getData();

});

function getData() {
	//get all the coins from the database
	$.ajax({
		type: "GET",
		dataType: "json",
		url: "./includes/get_data.php",

		success: function (response) {
			//An array is returned
			console.log(response);

			//calculate the total value of the whole wallet
			$.each(response, function (index, value) {

				//sum up all the rows in this each loop, extra + is to actual add the price to the existing variable
				console.log(response[index]["id"])
			});
		},
		error: function (response, error) {
			console.log('fouttttt', response, error);
		}
	});
}

