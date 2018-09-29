$(document).ready(function() {
	$(".dropdown-trigger").dropdown();
	$('select').formSelect();
	$('.sidenav').sidenav();
	$('.sidenav-activate').click(function() {

		$('.sidenav').sidenav('open');

	});
});