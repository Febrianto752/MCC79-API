const POKEMON_API_URL = "https://pokeapi.co/api/v2/pokemon";
const EMPLOYEE_API_URL = "https://localhost:7103/api/v1/employees";
//$.ajax({
//    url: POKEMON_API_URL,
//}).done((data) => {
//    let pokemonRow = "";
//    $.each(data.results, (key, val) => {
//        pokemonRow += `<tr>
//                  <td>${key + 1}</td>
//                  <td>${val.name}</td>
//                  <td><button onclick="detail('${val.url
//            }')" data-bs-toggle="modal" data-bs-target="#modal-pokemon" class="btn btn-primary">Detail</button></td>
//              </tr>`;
//    });
//    $("#tbody").html(pokemonRow);
//});

$(document).ready(function () {
    let table = $('#employee-table').DataTable({
        
        ajax: {
            url: EMPLOYEE_API_URL,
            dataType: "JSON",
            dataSrc: "data" //data source -> butuh array of object
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
                //render: function (data, type, row) {
                //    console.log("data : ", data);
                //    console.log("type : ", type);
                //    console.log("row : ", row);
                //    return `<button onclick="detail('${data}')" data-bs-toggle="modal" data-bs-target="#modal-pokemon" class="btn btn-primary">Detail</button>`;
                //}
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
                data: "detail",
                render: (data, type, row) => {
                    return `<button class="btn btn-info">Detail</button>`
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            'colvis',
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
            },
            {
                extend: 'copyHtml5',
                title: 'Copy table',
                text: 'Copy'
                //Columns to export
                //exportOptions: {
                //     columns: [0, 1, 2, 3, 4, 5, 6]
                //  }
            },
            
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
            const successMessageElem = `
      <div
          class="alert alert-success alert-dismissible fade alertMessage"
          role="alert"
      >
        <div class="text-center">
          ${result.message}       
        </div>

        <button
          type="button"
          class="btn-close"
          data-bs-dismiss="alert"
          aria-label="Close"
        ></button>
      </div>      
      `;

            $("#flashMessage").html(successMessageElem);
            $(".alertMessage").addClass("show");
            $('#employee-table').DataTable().ajax.reload();
        })
        .fail((error) => {
            console.log("Error : ", error);
            const errorMessages = error.responseJSON.errors;

            const listErrorElem = `
      <div
          class="alert alert-danger alert-dismissible fade alertMessage"
          role="alert"
      >
        <strong>Errors Message : </strong>
        <ul>
        ${errorMessages.map((errorMessage) => `<li>${errorMessage}</li>`)}
        </ul>

        <button
          type="button"
          class="btn-close"
          data-bs-dismiss="alert"
          aria-label="Close"
        ></button>
      </div>      
      `;

            $("#flashMessage").html(listErrorElem);
            $(".alertMessage").addClass("show");
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

//function detail(stringURL) {
//    loadingImgAnimation();
//    loadingDescriptionAnimation();

//    $("#nav-description-tab").trigger("click");
//    $.ajax({
//        url: stringURL,
//    }).done((res) => {
//        $(".modal-title").html(res.name.toUpperCase());

//        $("#pokemon-image").attr(
//            "src",
//            res.sprites.other["official-artwork"]["front_default"]
//        );

//        let pokemonTypes = "";

//        res.types.forEach((type) => {
//            pokemonTypes += `
//      <span class="badge rounded-pill bg-dark">${type.type.name}</span>
//      `;
//        });

//        $("#pokemon-type").html(pokemonTypes);

//        setDescriptionPokemon(res.species.url);
//        $("#height-description").html(`<b>Height</b> : ${res.height} decimetres`);
//        $("#weight-description").html(`<b>Weight</b> : ${res.weight} hectograms`);

//        // set abilities tab
//        let abilitieElems = "";

//        res.abilities.forEach((ability) => {
//            abilitieElems += `<li>${ability.ability.name}</li>`;
//        });

//        $("#ability-list").html(abilitieElems);

//        // set stats tab
//        $("#progress-hp").attr("style", `width: ${res.stats[0].base_stat}%`);
//        $("#progress-hp").html(`HP : ${res.stats[0].base_stat}`);

//        $("#progress-attack").attr("style", `width: ${res.stats[1].base_stat}%`);
//        $("#progress-attack").html(`ATTACK : ${res.stats[1].base_stat}`);

//        $("#progress-defense").attr("style", `width: ${res.stats[2].base_stat}%`);
//        $("#progress-defense").html(`DEFENSE : ${res.stats[2].base_stat}`);

//        $("#progress-speed").attr("style", `width: ${res.stats[3].base_stat}%`);
//        $("#progress-speed").html(`SPEED : ${res.stats[3].base_stat}`);

//        //removeLoadingAnimation();
//    });
//}

//$("#pokemon-image").on("load", function () {
//    console.log("Hello World");
//    removeLoadingImgAnimation();
//});

//function setDescriptionPokemon(stringUrl) {
//    $.ajax({
//        url: stringUrl,
//    }).done((res) => {
//        let pokemonDescription = "";

//        let flavorTextEntriesEN = res.flavor_text_entries.filter(
//            (entrie) => entrie.language.name == "en"
//        );

//        let uniqueFlavorTexts = Array.from(
//            new Set(
//                flavorTextEntriesEN.map((item) =>
//                    item.flavor_text
//                        .replace("\f", " ")
//                        .replace(/(\r\n|\n|\r)/gm, " ")
//                        .toLowerCase()
//                )
//            )
//        );

//        uniqueFlavorTexts.forEach((flavor_text) => {
//            pokemonDescription += `
//        <p>
//          ${flavor_text}
//        </p>
//      `;
//        });

//        $(".p-description").html(pokemonDescription);
//        removeLoadingDescriptionAnimation();
//    });
//}

//const loadingElem = `
//<div class="loading position-absolute bg-white h-100 d-flex justify-content-center align-items-center" style="top:0; width: 95%">
//  <div class="spinner-border " role="status">
//    <span class="visually-hidden">Loading...</span>
//  </div>
//</div>

//`;

//function loadingImgAnimation() {
//    $("#left-side-modal-pokemon").append(loadingElem);
//}

//function loadingDescriptionAnimation() {
//     $("#right-side-modal-pokemon").append(loadingElem);
//}

//function removeLoadingImgAnimation() {
//    $("#left-side-modal-pokemon .loading").remove();
//}

//function removeLoadingDescriptionAnimation() {
//    $("#right-side-modal-pokemon .loading").remove();
//}
