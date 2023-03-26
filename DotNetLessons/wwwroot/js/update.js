$(document).ready(function () {
    const DivContainer = $("#VacanciesModal-body");
    var displayedText;
    $('.applyDone').hide();
    $.ajax({
        method: "POST",
        url: "Handler.cs",
        data:
        {
            action: "showVacancies"
        },
        dataType: 'json',
        success: function (data) {
            $(data).each(function (index, data) {
                newDiv = $('<div>').addClass('VacantionPosition')
                    .appendTo(DivContainer);

                $('<div>')
                    .addClass('PositionTitle')
                    .html('<p>' + data.Title + '</p>')
                    .attr("style", "@charset 'utf-8'; float: left; overflow: hidden;")
                    .appendTo(newDiv);

                $('<div>')
                    .addClass('PositionMiniDescription')
                    .html('<p>' + data.Description + '</p>')
                    .attr("style", "@charset 'utf-8'; float: right; overflow: hidden; ")
                    .appendTo(newDiv);

                $('<a>')
                    .addClass('learnMore')
                    .val(data.EndDate)
                    .text('Learn more...')
                    .attr("style", "float:right; text-decoration: none; color: lightblue; cursor: pointer; font-family: monospace; font-size: 19px; font-weight: bold; margin-bottom: 100px; margin-right: 30px;")
                    .appendTo(newDiv);
            });
            $('.VacantionPosition').on('click', function () { // deschiderea meniului Vacantiei
                var title = $(this).find('.PositionTitle').text();
                var desc = $(this).find('.PositionMiniDescription').text();
                var date = $(this).find('.learnMore').val();
                $("#contact").css("display", "none");
                $('.VacanciesContent-Title').html('<h1>' + title + '</h1>');
                $('.VacanciesContent-EndTime').text('Deadline: ' + date);
                $('.VacanciesContent-Description').text(desc);

                //$(this).find()
                //alert($(this).find('.PositionTitle').text());
                // is open
                //$('.CV-PlacerBody').hide();
                $('.VacanciesModalDetails').show();
                $('#PlaceCV').on('click', function () {
                    $('.VacanciesContent').css({
                        'float': 'left',
                        'margin-left': '200px'
                    });
                    $('.CV-PlacerBody').show();
                    $('.formInput').val('');
                })

                $(document).on('click', function (e) {
                    if (!(($(e.target).closest(".VacanciesContent").length > 0) || ($(e.target).closest(".VacantionPosition").length > 0) || ($(e.target).closest(".CV-PlacerBody").length > 0))) {
                        $(".VacanciesModalDetails").hide();
                        $('.VacanciesContent').css({
                            'float': '',
                            'margin-left': ''
                        });
                        $('.CV-PlacerBody').hide();
                        $('.applyDone').hide();
                        $("#contact").css("display", "");
                    }
                });
                $('#UploadCV').on('change', function () {
                    $('.formLabel').text($('#UploadCV').val().split('\\').pop());
                })
                $('.formSubmit').on('click', function (e) {
                    //var file = $('#UploadCV')[0].files[0];
                    var formData = new FormData();
                    formData.append("file", $('#UploadCV')[0].files[0]);
                    //e.preventDefault();
                    //if ($('.formInputName').val() === null) {
                    //    alert('Introduceti Numele');
                    //} else if ($('.formInputSurname').val() === null) {
                    //    alert('Introduceti Prenumele');
                    //} else if ($('.formInputEmail').val() === null) {
                    //    alert('Introduceti Emailul');
                    //} else if ($('.formInputPhone').val() === null) {
                    //    alert('Introduceti Telefonul');
                    //} else if (!$('.formInputName').val('') || !$('.formInputSurname').val('') || !$('.formInputEmail').val('') || !$('.formInputPhone').val('') )
                    //{
                    formData.append("action", "setCandidat");
                    formData.append("VacancyTitle", title);

                    formData.append("Name", $('.formInputName').val());
                    formData.append("Surname", $('.formInputSurname').val());
                    formData.append("Email", $('.formInputEmail').val());
                    formData.append("Phone", $('.formInputPhone').val());

                    $.ajax({
                        type: "POST",
                        url: "Handler.ashx",
                        data: formData,
                        contentType: false,
                        processData: false,
                        cache: false,
                        //{
                        //    //action: "setCandidat",
                        //    //VacancyTitle: title,
                        //    //Name: $('.formInputName').val(),
                        //    //Surname: $('.formInputSurname').val(),
                        //    //Email: $('.formInputEmail').val(),
                        //    //Phone: $('.formInputPhone').val(),
                        //    formData
                        //},
                        success: function () {
                            $('.applyDone').show();
                            $('.formInput').val('');
                            $('#UploadCV').val('');
                            //alert("You are applied succesfuly");
                            //setTimeout($(document).click(), 300000);
                            //$(document).click();
                        }
                    });
                    //$.ajax({
                    //    method: "POST",
                    //    url: "/Handler.ashx",
                    //    data: formData,
                    //    success: function () {
                    //        $('.applyDone').show();
                    //        $('.formInput').val('');
                    //        //alert("You are applied succesfuly");
                    //        //setTimeout($(document).click(), 300000);
                    //        //$(document).click();
                    //    }
                    //});
                    //}
                    //alert(title + ' -space- ' + $('.formInputName').val() + ' --space-- ' + $('.formInputSurname').val() + '-- space --' + $('.formInputEmail').val() + ' -- space --' + $('.formInputPhone').val());
                })
                // is closed
            });
        }
    });

    //$('.learnMore').on('click', function () {
    //    alert($('.PositionTitle').text());
    //});

    $("#loginBtn").click(function () {
        $("#myModal").show();
        //$("#myModal").css('display', 'block');
    });
    $("#close").click(function () {
        $("#myModal").hide();
        //$("#myModal").css('display', 'none');
    });
    $(document).on('click', function (e) {
        if (!(($(e.target).closest(".modal-content").length > 0) || ($(e.target).closest("#loginBtn").length > 0))) {
            $("#myModal").hide();
        }
    });
    $('#UserLogin').on('click', function (event) {
        event.preventDefault();

        if ($("#uname").val() === "" || $("#pname").val() === "") {
            alert("Introduceti loginul sau parola");
            return false;
        }
        else {
            $.ajax({
                method: "POST",
                url: "Handler.ashx",
                data:
                {
                    action: "checkUser",
                    uname: $('#uname').val(),
                    pname: $('#pname').val()
                },
                dataType: 'json',
                success: function (data) {
                    if (JSON.stringify(data) === "[]") {
                        alert('incorect username or password');
                    } else {
                        $(data).each(function (index, dat) {
                            if (dat.RoleID === "1") {
                                window.location.href = 'AdminPanel.html';
                                //$("#loginform").attr('action', 'AdminPanel.aspx');
                            } else {
                                window.location.href = 'Login.html';
                                //$("#loginform").attr('action', 'Login.aspx');
                            }
                        })
                    }
                }
            });
        }
    });
})