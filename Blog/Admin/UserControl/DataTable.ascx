<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataTable.ascx.cs" Inherits="Blog.Admin.UserControl.DataTable" %>
<div class="container-fluid">
    <div class="row-fluid">
        <div id="sample_1_wrapper" class="dataTables_wrapper form-inline" role="grid">
            <div class="row-fluid">
                <div class="span6">
                    <div id="sample_1_length" class="dataTables_length">
                        <label>
                            条件:
                            <select size="1" name="sample_1_length" id="dataTables_status" aria-controls="sample_1" class="m-wrap small">
                                <option value="0" selected="selected">正常</option>
                                <option value="1">禁止</option>
                                <option value="-1">所有</option>
                            </select>
                        </label>
                    </div>
                </div>
                <div class="span6">
                    <div class="dataTables_filter" id="sample_1_filter">
                        <label>
                            搜索:
                            <input type="text" id="dataTable_key" value="请输入搜索关键词" aria-controls="sample_1" class="medium"></label>
                        <label>
                        <button class="btn blue" id="dataTable_search">Search</button></label>
                    </div>
                </div>
            </div>
            <table class="table table-striped table-bordered table-hover dataTable" id="sample_1" aria-describedby="sample_1_info">
                <thead>
                <tr role="row">
                    <th style="width: 24px;" class="sorting_disabled" role="columnheader" rowspan="1" colspan="1" aria-label="">
                    <div class="checker">
                    <span>
                    <input type="checkbox" class="group-checkable" data-set="#sample_1 .checkboxes">
                    </span>
                    </div>
                    </th>
                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Username: activate to sort column ascending" style="width: 159px;">Username</th>
                    <th class="hidden-480 sorting_disabled" role="columnheader" rowspan="1" colspan="1" aria-label="Email" style="width: 294px;">Email</th>
                    <th class="hidden-480 sorting" role="columnheader" tabindex="0" aria-controls="sample_1" rowspan="1" colspan="1" aria-label="Points: activate to sort column ascending" style="width: 124px;">Points</th>
                    <th class="hidden-480 sorting_disabled" role="columnheader" rowspan="1" colspan="1" aria-label="Joined" style="width: 190px;">Joined</th>
                    <th class="sorting_disabled" role="columnheader" rowspan="1" colspan="1" aria-label="" style="width: 168px;"></th>
                </tr>
                </thead>
                <tbody role="alert" aria-live="polite" aria-relevant="all">
                    <tr class="gradeX odd">
                        <td class=" sorting_1">
                        <div class="checker">
                        <span>
                        <input type="checkbox" class="checkboxes" value="1"></span>
                        </div>
                        </td>
                        <td class=" ">shuxer</td>
                        <td class="hidden-480 "><a href="mailto:shuxer@gmail.com">shuxer@gmail.com</a></td>
                        <td class="hidden-480 ">120</td>
                        <td class="center hidden-480 ">12 Jan 2012</td>
                        <td class=" "><span class="label label-success">Approved</span></td>
                    </tr>
                    <tr class="gradeX even">
                        <td class=" sorting_1">
                        <div class="checker">
                        <span>
                        <input type="checkbox" class="checkboxes" value="1"></span>
                        </div>
                        </td>
                        <td class="">looper</td>
                        <td class="hidden-480 "><a href="mailto:looper90@gmail.com">looper90@gmail.com</a></td>
                        <td class="hidden-480 ">120</td>
                        <td class="center hidden-480 ">12.12.2011</td>
                        <td class=""><span class="label label-warning">Suspended</span></td>
                    </tr>
                </tbody>
            </table>
            <div class="row-fluid">
                <div class="span12">
                    <div class="dataTables_info pull-left margin-right10 text-bottom span4" id="sample_1_info">
                    Showing 1 to 5 of 25 entries
                    </div>
                    <div class="dataTables_paginate paging_bootstrap pagination">
                        <ul class="pull-right">
                            <li class="prev disabled"><a href="#">← <span class="hidden-480">Prev</span></a></li>
                            <li class="active"><a href="#">1</a></li>
                            <li><a href="#">2</a></li>
                            <li><a href="#">3</a></li>
                            <li><a href="#">4</a></li>
                            <li><a href="#">5</a></li>
                            <li class="next"><a href="#"><span class="hidden-480">Next</span> → </a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    <script type="text/javascript">
        $(function () {
            var valide = 1;
            $u = "";
            $('#dataTables_status').change(function () {
                $id = $(this).val();
                $p = window.location.search.replace("?", "").split("&");
                $u = window.location.host + window.location.pathname;
                
                $u += '?';
                for (var i = 0; i < $p.length; i++) {
                    if ($p[i].indexOf("s=") > -1) {
                        $u += 's=' + $id;
                    } else {
                        $u += $p[i] + "&";
                    }
                }
                if ($u.indexOf("s=") == -1) {
                    $u += 's=' + $id;
                }
                window.location.href = 'http://' + $u;
            });
            $('.group-checkable').click(function () {
                if ($(this).attr("checked") == "checked") {
                    $(this).parent('span').addClass('checked');
                    $('.checkboxes').each(function () {
                        $(this).attr("checked", "checked");
                        $(this).parent('span').addClass('checked');
                    });
                } else {
                    $(this).parent('span').removeClass('checked');
                    $('.checkboxes').each(function () {
                        $(this).removeAttr("checked");
                        $(this).parent('span').removeClass('checked');
                    });
                }
            });
            $('.checkboxes').click(function () {
                if ($(this).attr("checked") == "checked") {
                    $(this).attr("checked", "checked");
                    $(this).parent('span').addClass('checked');
                } else {
                    $(this).removeAttr("checked");
                    $(this).parent('span').removeClass('checked');
                }
            });

            $('#dataTable_search').click(function () {
                $key = $('#dataTable_key').val();
                $rd = "";
                if ($key != "" && valide == 1) {

                    if ($key == "请输入搜索关键词") {
                        $parent = $(this).parent('label');
                        $parent.siblings('label').first().addClass('error');
                        $('#dataTable_key').val('请输入搜索关键词').addClass('error');
                        valide = 0;
                        return false;
                    }
                    $p = window.location.search;
                    $rd += window.location.host + window.location.pathname + "?";
                    $r = $p.replace("?", "").split("&");
                    if ($p != '') {
                        alert($r.length);
                        /*
                        if ($p.indexOf('p=') > -1 || $p.indexOf('s=') > -1) {
                            
                        }
                        for (var i = 0; i = $r.length; i++) {
                            
                        }
                        */
                    } else {
                        $rd += "q=" + encodeURI(encodeURI($key));
                    }

                    alert($rd);
                    return false;
                    window.location.href = "http://" + $rd;
                    
                } else {
                    $parent = $(this).parent('label');
                    $parent.siblings('label').first().addClass('error');
                    $('#dataTable_key').val('请输入搜索关键词').addClass('error');
                    valide = 0;
                    return false;
                }
            });
            $('#dataTable_key').focus(function () {
                if ($(this).val() == "请输入搜索关键词") {
                    $(this).val("");
                    $(this).removeClass('error');
                    $(this).parent('label').removeClass('error');
                }
            });
            $('#dataTable_key').blur(function () {
                if ($(this).val() == "" || $(this).val() == "请输入搜索关键词") {
                    $(this).val("请输入搜索关键词");
                } else {
                    valide = 1;
                }
            });
            $('#dataTables_status>option').each(function () {
                if ($(this).val() == getQueryString('s')) {
                    $(this).attr('selected', 'slected');
                } else {
                    $(this).removeAttr('selected');
                }
            });
        });
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
    </script>
    </div>
</div>
