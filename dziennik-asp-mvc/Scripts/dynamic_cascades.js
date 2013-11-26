$(document).ready(function () {

    $('#allGroups').change(
        function () {
            $.getJSON('/Grades/Subjects', {
                id: $(this).val(),
                ajax: 'true'
            }, function (data) {
                var html = '';
                var len = data.length;
                for (var i = 0; i < len; i++) {
                    console.log(data[i]);
                    html += '<option value="' + data[i].id_subject + '">'
                            + data[i].subject_name + '</option>';
                }
                html += '</option>';

                $('#subjectsInGroup').html(html);
                $('#s2id_subjectsInGroup').find('.select2-chosen').text(data[0].subject_name);
            });

            $.getJSON('/Grades/Students', {
                id: $(this).val(),
                ajax: 'true'
            }, function (data) {
                var html = '';
                var len = data.length;
                for (var i = 0; i < len; i++) {
                    console.log(data[i]);
                    html += '<option value="' + data[i].id_user + '">'
                    + data[i].full_name +'</option>';
                }
                html += '</option>';

                $('#studentsInGroup').html(html);
                $('#s2id_studentsInGroup').find('.select2-chosen').text(data[0].full_name);
            });
        });

    $('#subjectsInGroup').change(
        function () {
            console.log("selected: " + $('#subjectsInGroup option:selected').text() + " " + this.value);

        });
    $('#studentsInGroup').change(
       function () {
           console.log("selected: " + $('#studentsInGroup option:selected').text() + " " + this.value);

       });
});

$(function () {
    $("#gradeDate").datepicker({ dateFormat: 'dd-mm-yy' }).val();
});
