var Singleton = (function () {
    'use strict';
    var instance;
    var start = function () {
        console.log('start');
        getImages();
        actModal();
    }

    var actModal = function () {
        $('.actUploadModal').on('click', function () {
            console.log('actUploadModal');
            $('#uploadAdminModal').modal('show');
        });
        
        $('.actUpload').on('click', function () {
            postImage();
        });
    }

    var getImages = function () {
        $.ajax({
            url: '/admin/GetImages',
            type: 'GET',
            success: function (response) {
                console.log(response);
                var imageListHtml;
                $.each(response, function (idx, item) {
                    imageListHtml += 
                        '<tr>' +
                            '<td>' + item.id + '</td>' +
                            '<td>' + item.title + '</td>' +
                            '<td>' + item.description + '</td>' +
                            //'<td>' + item.imagepath + '</td>' +
                            //'<td>' + item.mimetype + '</td>' +
                            //'<td>' + item.size + '</td>' +
                            '<td>' + '<img src="/images/' + item.imagepath + '" width="200" />' + '</td>'
                        '</tr>';
                });
                $('#uploadtable').find('tbody').html(imageListHtml);
            },
            error: function (err) {
                console.log(err);
            }
        });
    }

    var postImage = function () {
        var fd = new FormData();
        var file = $('#imageinput')[0].files[0];
        fd.append('image', file);
        fd.append('title', $('#imagetitle').val());
        fd.append('description', $('#imagedescription').val());

        console.log(fd);
        $.ajax({
            url: '/admin/Upload',
            type: 'POST',
            data: fd,
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            success: function (response) {
                console.log(response);
                location.reload();
            },
            error: function (err) {
                console.log(err);
            }
        });
    }

    return {
        getInstance: function () {
            if (!instance) {
                start();
            }
        }
    }
})();
(function () {
    'use strict';
    Singleton.getInstance();
}());