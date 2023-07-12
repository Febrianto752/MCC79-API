const EMPLOYEE_API_URL = "https://localhost:7103/api/v1/employees";

$(document).ready(function () {
    $('#employee-table').DataTable({
        
        ajax: {
            url: EMPLOYEE_API_URL,
            dataType: "JSON",
            dataSrc: "data"
        },
        columns: [
            {
                data: "no",
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                data: "nik"
            },
            {
                data: 'fullname',
                render: (data, type, row) => {
                    return `${row.firstName} ${row.lastName}`;
                }
            },
            {
                data: "gender",
                render: (data, type, row) => {
                    return (data === 0) ? "Female" : "Male";
                }
            },
            {
                data: "email"
            },
            {
                data: "phoneNumber"
            },
            {
                data: "birthDate",
                render: (data, type, row) => {
                    return moment(data).format("dddd, DD-MM-YYYY");
                }
            },
            {
                data: "delete",
                render: (data, type, row) => {
                    return `<button class="btn btn-danger" onclick="Delete('${row.guid}')">Delete</button>`
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            'colvis',
            
            {
                extend: 'copyHtml5',
                title: 'Copy table',
                text: 'Copy'
                //Columns to export
                //exportOptions: {
                //     columns: [0, 1, 2, 3, 4, 5, 6]
                //  }
            },
            {
                extend: 'collection',
                text: 'Export',
                buttons: [
                {
                    extend: 'excelHtml5',
                    title: 'Excel',
                    text: 'Export to excel'
                    //Columns to export
                    //exportOptions: {
                    //     columns: [0, 1, 2, 3,4,5,6]
                    // }
                },
                {
                    extend: 'pdfHtml5',
                    title: 'PDF',
                    text: 'Export to PDF',
                    //Columns to export
                    //exportOptions: {
                    //     columns: [0, 1, 2, 3, 4, 5, 6]
                    //  }
                },
                {
                    extend: 'csvHtml5',
                    title: 'Table Employee',
                    text: "Export to CSV"
                }]
            }
            
        ]
    });

    $("#btnCreateEmployee").on("click", () => {
        Insert();       
    }); 
});

function Insert() {
    const employee = {
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        birthDate: $("#birthDate").val(),
        gender: parseInt($('input[name="gender"]:checked').val()),
        hiringDate: $("#hiringDate").val(),
        email: $("#email").val(),
        phoneNumber: $("#phoneNumber").val(),
    };
    console.log("employee : ", employee);

    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: EMPLOYEE_API_URL,
        type: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        data: JSON.stringify(employee), //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
    })
        .done((result) => {
            console.log("result : ", result);
            //buat alert pemberitahuan jika success
            Swal.fire({
                title: result.message,
                icon: 'success',
                confirmButtonText: 'OK'
            })

            // reload table 
            $('#employee-table').DataTable().ajax.reload();
        })
        .fail((error) => {
            console.log("Error : ", error);

            Swal.fire({
                title: error.responseJSON.message,
                icon: 'error',
                confirmButtonText: 'OK'
            })
        })
        .always(() => {
            $("#firstName").val("");
            $("#lastName").val("");
            $("#birthDate").val("2000-01-02");
            $("input[name='gender'][value='female']").prop("checked", true);
            $("#hiringDate").val("2022-01-02");
            $("#email").val("");
            $("#phoneNumber").val(0);
        });
}

function Delete(guid) {

    const agreeDeleteData = confirm("are you sure ? ");

    if (agreeDeleteData) {
        $.ajax({
            url: EMPLOYEE_API_URL + `?guid=${guid}`,
            type: "DELETE",
            headers: {
                "Content-Type": "application/json",
            }
        })
        .done((result) => {
            console.log("result : ", result);
            //buat alert pemberitahuan jika success
            Swal.fire({
                title: result.message,
                icon: 'success',
                confirmButtonText: 'OK'
            })

            // reload table 
            $('#employee-table').DataTable().ajax.reload();
        })
        .fail((error) => {
            console.log("Error : ", error);

            Swal.fire({
                title: error.responseJSON.message,
                icon: 'error',
                confirmButtonText: 'OK'
            })
        });
    }
    
}

