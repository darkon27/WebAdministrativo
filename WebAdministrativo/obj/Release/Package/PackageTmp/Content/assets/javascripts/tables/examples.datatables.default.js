/*
Name: 			Tables / Advanced - Examples
Written by: 	Okler Themes - (http://www.okler.net)
Theme Version: 	1.3.0
*/

(function( $ ) {

	'use strict';

	var datatableInit = function() {			
		$('#datatable-default').DataTable({
			searching: false, // Desactiva la búsqueda
			lengthChange: false, // Desactiva el control de "Show"
			language: {
				"sInfo": "Mostrando _START_ a _END_ de _TOTAL_ registros"
			}
		});
	};

	$(function() {
		datatableInit();
	});

}).apply( this, [ jQuery ]);