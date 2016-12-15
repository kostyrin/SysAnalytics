using System;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using NHibernate.Mapping;
using SysAnalytics.Model.Enums;
using SysAnalytics.Web.ViewModels;

namespace SysAnalytics.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static string CurrentVersion(this HtmlHelper helper)
        {
            try
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return version.ToString();
            }
            catch
            {
                return "?.?.?.?";
            }
        }

        public static string CurrentYear(this HtmlHelper helper)
        {
            return DateTime.Now.Year.ToString();
        }

        public static MvcHtmlString CreatejqGridHtml<T>(this HtmlHelper helper, GridViewModel<T> model)
        {
            var htmlBuilder = new StringBuilder();
            htmlBuilder.AppendFormat(@"<table id=""{0}"" class=""scroll"" cellpadding=""0"" cellspacing=""0""></table>", model.Id);
            htmlBuilder.AppendFormat(@"<div id=""{0}Pager"" class=""scroll"" style=""text-align:center;""></div>", model.Id);
            return MvcHtmlString.Create(htmlBuilder.ToString());
        }

        public static MvcHtmlString CreatejqGridTemplate<T>(this HtmlHelper helper, GridViewModel<T> model)
        {
            var htmlBuilder = new StringBuilder();
            foreach (var t in model.Templates)
            {
                htmlBuilder.AppendFormat(@"<li>");
                htmlBuilder.AppendFormat(@"<span>{0}</span>", t.Name);
                htmlBuilder.AppendFormat(@"<button class=""tpl-remove-btn"" type=""button"" data-tpl=""{0}"">Remove</button>", t.Name);
                htmlBuilder.AppendFormat(@"<button class=""tpl-apply-btn"" type=""button"" data-tpl=""{0}"">Apply</button>", t.Name);
                htmlBuilder.AppendFormat(@"</li>", t.Name);
            }
            return MvcHtmlString.Create(htmlBuilder.ToString());
        }

        /// <summary>
        /// http://www.ok-soft-gmbh.com/jqGrid/WorkingColumnChooser.htm
        /// http://www.codeproject.com/Tips/784342/Export-Data-from-jqGrid-into-a-real-Excel-File
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static MvcHtmlString CreatejqGridJS<T>(this HtmlHelper helper, GridViewModel<T> model)
        {
            var htmlBuilder = new StringBuilder();

            htmlBuilder.AppendFormat(@"<script type=""text/javascript"">");
            //htmlBuilder.AppendFormat(@"var dataExport;");
            htmlBuilder.AppendFormat(@"$(document).ready(function ()");
            htmlBuilder.AppendFormat(@"{{");

            htmlBuilder.AppendFormat(@"
                var datePick = function (elem) {{
				  setTimeout(function () {{
					  $(elem).datepicker({{
						  dateFormat: 'dd-M-yy',
						  autoSize: true,
						  changeYear: true,
						  changeMonth: true,
						  showButtonPanel: true,
						  showWeek: true,
						  onSelect: function () {{
							  var that = this;
							  if (this.id.substr(0, 3) === 'gs_') {{
								  setTimeout(function () {{
									  that.triggerToolbar();
								  }}, 50);
							  }} else {{
								  $(this).trigger('change');
							  }}
						  }}
					  }});
				  }}, 100);
				}};");

            htmlBuilder.AppendFormat(@"
                function formateadorLink(cellvalue, options, rowObject) {{
                    return '<a href=/{0}/Edit/'+ cellvalue + '>' + cellvalue + '</a>';
               }}", model.Caption);

            htmlBuilder.AppendFormat(@"
                function formateadorEdit(cellvalue, options, rowObject) {{
                    return '<a href=/{0}/Edit/'+ cellvalue + '><button>Edit</button></a>';
               }}", model.Caption);

            htmlBuilder.AppendFormat(@"
                function formateadorDetails(cellvalue, options, rowObject) {{
                    return '<a href=/{0}/Details/'+ cellvalue + '><button>Details</button></a>';
               }}", model.Caption);

            htmlBuilder.AppendFormat(@"
                $('#tpl-name').on('input', function() {{
                  $(this).parent().removeClass('has-error');
                  $('#tpl-name-error').html('').addClass('hidden');
                }});");

            htmlBuilder.AppendFormat(@"
                var numberTemplate = {{
					formatter: 'number',
					align: 'right',
					sorttype: 'number',
					searchoptions: {{
						searchhidden: true,
						sopt: ['eq', 'ne', 'lt', 'gt', 'nu', 'nn']
					}}
				}};");

            htmlBuilder.AppendFormat(@"
                            var cm = [");
            foreach (var column in model.Columns)
            {
                htmlBuilder.AppendFormat(@"{{ name: '{0}', index: '{0}', width: {1}, align: '{2}', sortable: {3}, hidden: {4},", column.Name.Replace("_", " ")
                    , column.Width > 0 ? column.Width : 80
                    , column.Align == null ? "left" : column.Align.ToLower()
                    , column.IsSortable.ToString().ToLower()
                    , column.IsHidden.ToString().ToLower()
                    );
                if (!string.IsNullOrEmpty(column.Formatter))
                {
                    //htmlBuilder.AppendFormat(@"formatter: '{0}', ", column.Formatter);
                    switch (column.Formatter)
                    {
                        case JSFormatters.Date:
                            htmlBuilder.AppendFormat(@"formatter: '{0}', ", column.Formatter);
                            htmlBuilder.AppendFormat(@"formatoptions: {{ srcformat: 'm/d/Y h:i:s', newformat: 'm/d/Y' }},");
                            //htmlBuilder.AppendFormat(@"sorttype:'date',");
                            htmlBuilder.AppendFormat(@"searchoptions:{{ searchhidden: true, sopt: ['eq', 'lt', 'le', 'gt', 'ge'], dataInit:datePick, attr:{{title:'Select Date'}}}},");
                            break;
                        case JSFormatters.Number:
                            htmlBuilder.AppendFormat(@"template: numberTemplate,");
                            break;
                        case JSFormatters.Integer:
                            htmlBuilder.AppendFormat(@"searchoptions:{{ searchhidden: true, sopt: ['eq', 'ne', 'lt', 'le', 'gt', 'ge', 'nu', 'nn']}},");//, 'ni', 'ni'
                            break;
                        case JSFormatters.Select:
                            htmlBuilder.AppendFormat(@"stype: 'select',");
                            htmlBuilder.AppendFormat(@"searchoptions:{{ searchhidden: true, sopt: ['eq', 'ne', 'nu', 'nn'], value:'{0}'}},", StringEnum.GetEnumsForJson(column.TypeName));
                            break;
                        case JSFormatters.Checkbox:
                            htmlBuilder.AppendFormat(@"formatter: '{0}', ", column.Formatter);
                            htmlBuilder.AppendFormat(@"stype: 'select',");
                            htmlBuilder.AppendFormat(@"searchoptions:{{ searchhidden: true, sopt: ['eq', 'ne'], value: ' true:Yes; false:No' }},");
                            break;
                        case JSFormatters.Link:
                            htmlBuilder.AppendFormat(@"formatter: formateadorLink, ");
                            //htmlBuilder.AppendFormat(@"stype: '{0}',", column.Formatter);
                            //htmlBuilder.AppendFormat(@"searchoptions:{{ searchhidden: true, sopt: ['eq', 'ne'], value: ' true:Yes; false:No' }},");
                            break;
                        case JSFormatters.ButtonEdit:
                            htmlBuilder.AppendFormat(@"formatter: formateadorEdit, ");
                            break;
                        case JSFormatters.ButtonDetails:
                            htmlBuilder.AppendFormat(@"formatter: formateadorDetails, ");
                            break;
                        default:
                            htmlBuilder.AppendFormat(@"searchoptions:{{ searchhidden: true, sopt: ['eq', 'ne', 'cn', 'nc', 'le', 'gt', 'ge', 'nu', 'nn']}},");
                            break;
                    }
                }
                else htmlBuilder.AppendFormat(@"searchoptions:{{ searchhidden: true, sopt: ['eq', 'ne', 'cn', 'nc', 'nu', 'nn']}},");
                //, column.Formatter == null ? "text" : column.Formatter.ToLower()
                //htmlBuilder.AppendFormat(@"searchoptions: {{searchhidden: true}},");
                htmlBuilder.AppendFormat(@"}},");
            }
            htmlBuilder.AppendFormat(@"];");

            htmlBuilder.AppendFormat(@"
                var saveObjectInTemplates = function(t, e) {{
                    $('#loader').show();
					$.ajax({{
						type: 'post',
						url: '{1}',
						contentType: 'application/json; charset=utf-8',
						data: JSON.stringify({{
							templname: t,
							content: e,
                            action: 'apply'
						}}),
						success: function() {{
								templates[t] = e;
								var li = document.createElement('li'),
									tplName = document.createElement('span'),
									applyBtn = document.createElement('button'),
									removeBtn = document.createElement('button');

								$(applyBtn).attr('type', 'button').data('tpl', t).html('Apply').addClass('tpl-apply-btn');
								$(removeBtn).attr('type', 'button').data('tpl', t).html('Remove').addClass('tpl-remove-btn');
								$(tplName).html(t);

								$(li).append($(tplName));
								$(li).append($(removeBtn));
								$(li).append($(applyBtn));

								$('.jqGrid_wrapper .templates ul').append($(li));
								$(applyBtn).on('click', function() {{
									$(this).parents('.templates').parent().detach();
									$('#{0}').jqGrid('GridUnload');
									myColumnStateName = $(this).data('tpl').toString();
									myColumnState = restoreColumnState(cm);
									createGrid();
									$('#{0}').jqGrid('remapColumns', myColumnState.permutation, true);
								}});

								$(removeBtn).on('click', function() {{
								  removeTpl($(this).data('tpl'));
								}});
                                $('#loader').hide();
						}}
					}});
				  }};", model.Id, model.UrlTempl);

            htmlBuilder.AppendFormat(@"
                var loadObjectFromTemplates = function(e) {{
					return templates[myColumnStateName]
				  }};");

            htmlBuilder.AppendFormat(@"
                var saveColumnState = function(perm) {{
					var colModel = $('#{0}').jqGrid('getGridParam', 'colModel'),
						i,
						l = colModel.length,
						colItem, cmName,
						postData = $('#{0}').jqGrid('getGridParam', 'postData'),
						columnsState = {{
							search: $('#{0}').jqGrid('getGridParam', 'search'),
							page: $('#{0}').jqGrid('getGridParam', 'page'),
							sortname: $('#{0}').jqGrid('getGridParam', 'sortname'),
							sortorder: $('#{0}').jqGrid('getGridParam', 'sortorder'),
							permutation: perm,
							colStates: {{}}
						}},
						colStates = columnsState.colStates;

					if (typeof (postData.filters) !== 'undefined') {{
						columnsState.filters = postData.filters;
					}}

					for (i = 0; i < l; i++) {{
						colItem = colModel[i];
						cmName = colItem.name;
						if (cmName !== 'rn' && cmName !== 'cb' && cmName !== 'subgrid') {{
							colStates[cmName] = {{
								width: colItem.width,
								hidden: colItem.hidden
							}};
						}}
					}}
					saveObjectInTemplates(myColumnStateName, columnsState);
				  }};", model.Id);

            htmlBuilder.AppendFormat(@"
                var restoreColumnState = function (colModel) {{
					var colItem,
						i,
						l = colModel.length,
						colStates,
						cmName,
						columnsState = loadObjectFromTemplates(myColumnStateName);

					if (columnsState) {{
						colStates = columnsState.colStates;
						for (i = 0; i < l; i++) {{
							colItem = colModel[i];
							cmName = colItem.name;
							if (cmName !== 'rn' && cmName !== 'cb' && cmName !== 'subgrid') {{
								colModel[i] = $.extend(true, {{}}, colModel[i], colStates[cmName]);
							}}
						}}
					}}
					return columnsState;
				  }};");

            htmlBuilder.AppendFormat(@"var myColumnStateName;");
            htmlBuilder.AppendFormat(@"var myColumnState;");

            htmlBuilder.AppendFormat(@"
                function removeTpl(tplToRemove) {{
                    $('#loader').show();
					$.ajax({{
						type: 'post',
						url: '{0}',
						contentType: 'application/json; charset=utf-8',
						data: JSON.stringify({{
							templname: tplToRemove, action:'remove'
						}}),
						success: function() {{
						      $('.jqGrid_wrapper .templates button.tpl-remove-btn').each(function() {{
							    if ($(this).data('tpl') === tplToRemove) {{
							      $(this).parents('li').remove();
							    }}
						      }});
                            $('#remove-tpl-success').show();
                            $('#remove-tpl-success .btn').on('click', function() {{
                                $('#remove-tpl-success').hide();
                            }});  
                            $('#loader').hide();
						}}
					}});
				  }}", model.UrlTempl);

            htmlBuilder.AppendFormat(@"createGrid();");

            htmlBuilder.AppendFormat(@"
                  $('#send-report-btn').on('click', function() {{
                        $('#send-report-btn').prop('disabled', true).css({{ 'min-width': 184  }});
                        $('#send-report-btn').html('<i class=""fa fa-spinner fa-spin""></i>');
                        var colModel = $('#{0}').jqGrid('getGridParam', 'colModel'),
                            i,
                            l = colModel.length,
                            colItem,
                            cmName,
                            postData = $('#{0}').jqGrid('getGridParam', 'postData'),
                            columnsState = {{
                                search: $('#{0}').jqGrid('getGridParam', 'search'),
                                page: $('#{0}').jqGrid('getGridParam', 'page'),
                                sortname: $('#{0}').jqGrid('getGridParam', 'sortname'),
                                sortorder: $('#{0}').jqGrid('getGridParam', 'sortorder'),
                                permutation: document.getElementById('{0}').p.remapColumns,
                                colStates: {{}}
                            }},
                            colStates = columnsState.colStates;

                        if (typeof (postData.filters) !== 'undefined') {{
                            columnsState.filters = postData.filters;
                        }}

                        for (i = 0; i < l; i++) {{
                            colItem = colModel[i];
                            cmName = colItem.name;
                            
                            if (cmName !== 'rn' && cmName !== 'cb' && cmName !== 'subgrid') {{
                                colStates[cmName] = {{
                                    width: colItem.width,
                                    hidden: colItem.hidden,
                                    formatter: colItem.formatter
                                }};
                            }}
                        }}
                        $.ajax({{  
                                    type: 'post',   
                                    url: '/{1}/ExportAndSend',   
                                    data: JSON.stringify({{ columnsState : columnsState, action: 'send'}}),
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    success: function (data, text) {{
                                        $('#send-report-btn').prop('disabled', false).css({{
                                          'min-width': 184
                                        }});
                                        $('#send-report-btn').html('Send report to my email');
                                        if (data.Success) {{
                                                    //This is for the example. Please do something prettier for the user, :)
                                                    alert('File was sending');                                           
                                                }}
                                                else {{
                                                    alert('Oups.. we had errors: ' + data.ErrorMessage);
                                                }}
                                    }},
                                    error: function (request, status, error) {{
                                        alert(request.responseText);
                                    }} 
                               }});
                        }});", model.Id, model.Caption);

            htmlBuilder.AppendFormat(@"
                $('.templates button.tpl-apply-btn').on('click', function() {{
					  $(this).parents('.templates').parent().detach();
					  $('#{0}').jqGrid('GridUnload');
					  myColumnStateName = $(this).data('tpl').toString();
					  myColumnState = restoreColumnState(cm);
					  createGrid();
					  $('#{0}').jqGrid('remapColumns', myColumnState.permutation, true);
				  }});", model.Id);

            htmlBuilder.AppendFormat(@"
                $('button.tpl-remove-btn').on('click', function() {{
					removeTpl($(this).data('tpl'));
				  }});");

            htmlBuilder.AppendFormat(@"$('#save-tpl-message button').on('click', function() {{
					  '1' === $(this).val() ? ($('#save-tpl-message').hide(), $('#save-tpl-form').show()) : $('#save-tpl').hide()
				  }});");

            htmlBuilder.AppendFormat(@"$('#save-tpl-form button').on('click', function(){{
					if ($(this).attr('type') === 'button') {{
					  $('#save-tpl').css('display', 'none');
					}}
				  }});");


            htmlBuilder.AppendFormat(@"function createGrid() {{");
            htmlBuilder.AppendFormat(@"$('#{0}').jqGrid({{", model.Id);
            htmlBuilder.AppendFormat(@"url: '{0}',", model.Url);
            htmlBuilder.AppendFormat(@"loadComplete: function () {{ $('option[value = 100000000]').text('All'); }},");
            htmlBuilder.AppendFormat(@"datatype: 'json',");
            htmlBuilder.AppendFormat(@"mtype: 'POST',");
            htmlBuilder.AppendFormat(@"autowidth:true, shrinkToFit:false,");
            htmlBuilder.AppendFormat(@"colModel: cm,");
            htmlBuilder.AppendFormat(@"pager: jQuery('#{0}Pager'),", model.Id);
            htmlBuilder.AppendFormat(@"loadonce: false,");
            htmlBuilder.AppendFormat(@"rowNum: 20,");
            htmlBuilder.AppendFormat(@"rowList: [20, 50, 100, 500, 1000, 2000, 3000, 4000],");
            htmlBuilder.AppendFormat(@"viewrecords: true,");
            htmlBuilder.AppendFormat(@"imgpath: '/scripts/themes/coffee/images',");
            htmlBuilder.AppendFormat(@"page: myColumnState ? myColumnState.page : 1,
              search: myColumnState ? myColumnState.search : false,
              postData: myColumnState ? {{ filters: myColumnState.filters }} : {{}},");
            htmlBuilder.AppendFormat(@"caption: '{0}',", model.Caption);
            htmlBuilder.AppendFormat(@"width: {0},", model.Width);
            htmlBuilder.AppendFormat(@"height: {0}", model.Height);
            htmlBuilder.AppendFormat(@"}})");
            htmlBuilder.AppendFormat(@".navGrid('#{0}Pager', {{ view: false, del: false, add: false, edit: false }},", model.Id);
            htmlBuilder.AppendFormat(@"{{}},");
            htmlBuilder.AppendFormat(@"{{}},");
            htmlBuilder.AppendFormat(@"{{}},");
            htmlBuilder.AppendFormat(@"{{closeOnEscape: true, multipleSearch: true, closeAfterSearch: true, multipleGroup: true, showQuery: true,");
            htmlBuilder.AppendFormat(@"recreateFilter: true,");
            htmlBuilder.AppendFormat(@"
                                        afterShowSearch: function() {{
                                            $('div#searchmodfbox_{0} table.group.ui-widget.ui-widget-content td.columns select').each(function() {{
                                              var columnsOptions = [],
                                                  selectedOpt;
                                              $(this).find('option').each(function() {{
                                                columnsOptions.push($(this).val());
                                                if ($(this).prop('selected') === true) {{
                                                  selectedOpt = $(this).val();
                                                }}
                                              }});
                                              columnsOptions = columnsOptions.sort();
                                              $(this).find('option').each(function(i) {{
                                                $(this).val(columnsOptions[i]);
                                                $(this).text(columnsOptions[i]);
                                                if (columnsOptions[i] === selectedOpt) {{
                                                  $(this).prop('selected', true);
                                                }}
                                              }});
                                            }});
                                          }},", model.Id);
            htmlBuilder.AppendFormat(@"
                                        afterRedraw: function() {{
                                            $('div#searchmodfbox_{0} table.group.ui-widget.ui-widget-content td.columns select').each(function() {{
                                              var columnsOptions = [],
                                                  selectedOpt;
                                              $(this).find('option').each(function() {{
                                                columnsOptions.push($(this).val());
                                                if ($(this).prop('selected') === true) {{
                                                  selectedOpt = $(this).val();
                                                }}
                                              }});
                                              columnsOptions = columnsOptions.sort();
                                              $(this).find('option').each(function(i) {{
                                                $(this).val(columnsOptions[i]);
                                                $(this).text(columnsOptions[i]);
                                                if (columnsOptions[i] === selectedOpt) {{
                                                  $(this).prop('selected', true);
                                                }}
                                              }});
                                            }});
                                          }},", model.Id);
            htmlBuilder.AppendFormat(@"
                                        onSearch: function () {{
                                            var remapCol = this.p.remapColumns;
                                            $('#save-tpl').show();
                                            $('#save-tpl-form').on('submit', function(event) {{
                                              event.preventDefault();
                                              var newTplNameValid;
                                              if ($('.jqGrid_wrapper .templates .tpl-apply-btn').length > 0) {{
                                                  $('.jqGrid_wrapper .templates .tpl-apply-btn').each(function() {{
                                                    if ($('#tpl-name').val().toLowerCase() === $(this).data('tpl').toLowerCase()) {{
                                                      newTplNameValid = 0;
                                                      $('#tpl-name').parent().addClass('has-error');
                                                      $('#tpl-name-error').html($('#tpl-name').data('error-exists')).removeClass('hidden');
                                                      return false;
                                                    }}
                                                    else {{
                                                      newTplNameValid = 1;
                                                    }}
                                                  }});  
                                              }}
                                              else {{
                                                newTplNameValid = 1;
                                              }}             
                                              if ($('#tpl-name').val().length > 0 && newTplNameValid === 1) {{
                                                $('#save-tpl').hide();
                                                $('#save-tpl-message').show();
                                                $('#save-tpl-form').hide().off('submit');
                                                myColumnStateName = $('#tpl-name').val();
                                                saveColumnState(remapCol);
                                              }}
                                            }});
                                          }}");
            htmlBuilder.AppendFormat(@"}},");
            htmlBuilder.AppendFormat(@"{{}})");

            htmlBuilder.AppendFormat(@"
                .jqGrid('navButtonAdd', '#{0}Pager', {{
                  caption: 'Columns',
                  title: 'Reorder Columns',
                  onClickButton: function() {{
                    $('#colchooser_{0} ~ .templates').remove();
                    var tpls = $('.templates').clone(true);
                    setTimeout(function() {{
                        tpls.insertAfter($('#colchooser_{0}')), tpls.show();
                        tpls.find('.tpl-remove-btn').on('click', function() {{
                            $(this).parents('li').remove();
                        }});
                    }}, 100);
                    $('#{0}').jqGrid('columnChooser', {{
                      done: function(permutationObject) {{
                        if (permutationObject) {{
                          $('#{0}').jqGrid('remapColumns', permutationObject, false);
                          $('#save-tpl').show();
                          
                          $('#save-tpl-form').on('submit', function(event) {{
                            event.preventDefault();
                            var newTplNameValid;
                            if ($('.jqGrid_wrapper .templates .tpl-apply-btn').length > 0) {{
                                $('.jqGrid_wrapper .templates .tpl-apply-btn').each(function() {{
                                  if ($('#tpl-name').val().toLowerCase() === $(this).data('tpl').toLowerCase()) {{
                                    newTplNameValid = 0;
                                    $('#tpl-name').parent().addClass('has-error');
                                    $('#tpl-name-error').html($('#tpl-name').data('error-exists')).removeClass('hidden');
                                    return false;
                                  }} else {{
                                    newTplNameValid = 1;
                                  }}
                                }});
                            }}
                            else {{
                                newTplNameValid = 1;
                            }}   
                            if ($('#tpl-name').val().length > 0 && newTplNameValid === 1) {{
                              $('#save-tpl').hide();
                              $('#save-tpl-message').show();
                              $('#save-tpl-form').hide().off('submit');
                              myColumnStateName = $('#tpl-name').val();
                              saveColumnState(permutationObject);
                            }}
                          }});

                        }}
                      }}
                    }});
                  }}
                }})", model.Id);

            htmlBuilder.AppendFormat(@"
                .jqGrid('navButtonAdd', '#{0}Pager', {{
					  caption: 'Excel',
					  buttonicon: 'ui-icon-bookmark',
					  onClickButton: function() {{
						  ExportDataToExcel('#{0}');
					  }},
					  position: 'last'
					}})", model.Id);
            htmlBuilder.AppendFormat(@";");
            htmlBuilder.AppendFormat(@"}};
                                            ");

            htmlBuilder.AppendFormat(@"function ExportDataToExcel(tableCtrl) {{ ExportJQGridDataToExcel(tableCtrl, '{0}.xlsx'); }}", model.Caption);
            //htmlBuilder.AppendFormat(@"var datePick = function(elem) {{ jQuery(elem).datepicker({{ dateFormat: 'dd/M/yy', autoSize: true,");
            //htmlBuilder.AppendFormat(@"changeYear: true, changeMonth: true, showWeek: true, showButtonPanel: true}});}},");
            //htmlBuilder.AppendFormat(@"numberTemplate = {{formatter: 'number', align: 'right', sorttype: 'number',");
            //htmlBuilder.AppendFormat(@"searchoptions: {{ searchhidden: true, sopt: ['eq', 'ne', 'lt', 'gt', 'nu', 'nn'] }}");
            //htmlBuilder.AppendFormat(@"}}");
            //htmlBuilder.AppendFormat(@";");
            htmlBuilder.AppendFormat(@"$(window).bind('resize', function () {{ var width = $('.jqGrid_wrapper').width(); $('{0}').setGridWidth(width); }});", model.Id);
            //htmlBuilder.AppendFormat(@"var templates={{'test1':{{search:true,page:1,sortname:'',sortorder:'asc',permutation:[0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152],colStates:{{OrderId:{{width:50,hidden:false}},CustomerId:{{width:50,hidden:false}},WriterId:{{width:50,hidden:false}},PrefferedWriter:{{width:50,hidden:true}},StatusRaw:{{width:50,hidden:true}},'Major2 Name':{{width:80,hidden:true}},AssignDate:{{width:70,hidden:true}},DeletedDate:{{width:70,hidden:true}},CompletionDate:{{width:70,hidden:true}},DeadlineDate:{{width:70,hidden:true}},WriterDeadlineDate:{{width:70,hidden:true}},CreateDate:{{width:70,hidden:true}},AvailableDate:{{width:70,hidden:true}},LastRevisionDate:{{width:70,hidden:true}},PaymentDate:{{width:70,hidden:true}},DeadlineHours:{{width:50,hidden:true}},StudyLevel:{{width:80,hidden:true}},AssignmentType:{{width:80,hidden:true}},DeadlineHistory:{{width:80,hidden:true}},Revised:{{width:50,hidden:true}},RevCount:{{width:50,hidden:true}},PaymentStatus:{{width:80,hidden:true}},PaymentTrackingNumber:{{width:80,hidden:true}},PaymentChargeback:{{width:80,hidden:true}},PagePrice:{{width:50,hidden:true}},WriterPagePrice:{{width:50,hidden:true}},NumPages:{{width:50,hidden:true}},DiscountCode:{{width:80,hidden:true}},WriterBonus:{{width:50,hidden:true}},NumSources:{{width:50,hidden:true}},Subject:{{width:80,hidden:true}},Topic:{{width:80,hidden:true}},Books:{{width:80,hidden:true}},Description:{{width:70,hidden:true}},CitationStyle:{{width:80,hidden:true}},English:{{width:80,hidden:true}},NeedPlagiarismReport:{{width:50,hidden:true}},Spacing:{{width:80,hidden:true}},NativeLanguage:{{width:50,hidden:true}},TitlePage:{{width:50,hidden:true}},Outline:{{width:50,hidden:true}},Bibliography:{{width:50,hidden:true}},DeadlineFixed:{{width:50,hidden:true}},AdditionalMaterials:{{width:50,hidden:true}},TotalCost:{{width:50,hidden:true}},TotalWithDiscount:{{width:50,hidden:true}},DiscountTotal:{{width:50,hidden:true}},Currency:{{width:80,hidden:true}},IsSurveyFilled:{{width:50,hidden:true}},NotifiedLate:{{width:50,hidden:true}},ForcedlySkipPlagreport:{{width:50,hidden:true}},Risk:{{width:80,hidden:true}},WasWPPRaisedDueHighRisk:{{width:50,hidden:true}},LowPriceForHighRiskOrderWarnStatus:{{width:80,hidden:true}},OtherCitation:{{width:80,hidden:true}},CanSupportSeePartnerOrder:{{width:50,hidden:true}},CanPartnerSeeOrder:{{width:50,hidden:true}},NumberOfSlides:{{width:50,hidden:true}},WasOpenForEditBefore:{{width:50,hidden:true}},Abstract:{{width:50,hidden:true}},AssignedBy:{{width:50,hidden:true}},Archived:{{width:50,hidden:true}},CanExtendDeadline:{{width:50,hidden:true}},AnaliticScenario:{{width:80,hidden:true}},'Customer Employed':{{width:80,hidden:true}},'Customer MainMajors':{{width:80,hidden:true}},'Customer StudyLevel':{{width:80,hidden:true}},'Customer CurrentGPA':{{width:50,hidden:true}},'Customer DesiredGPA':{{width:50,hidden:true}},'Customer EnglishNative':{{width:50,hidden:true}},'Customer EnglishStudyYears':{{width:50,hidden:true}},'Customer Difficulties':{{width:80,hidden:true}},'Customer CommonMistakes':{{width:80,hidden:true}},'Customer CustomerQualityExpectations':{{width:80,hidden:true}},'Customer AdditionalComments':{{width:80,hidden:true}},'Customer IsWroteReview':{{width:50,hidden:true}},'Customer SendTips':{{width:50,hidden:true}},'Customer SendSeasonal':{{width:50,hidden:true}},'Customer NumCompletedOrders':{{width:50,hidden:true}},'Customer NumCompletedPages':{{width:50,hidden:true}},'Customer Bonus':{{width:50,hidden:true}},'Customer Emergency':{{width:50,hidden:true}},'Customer IsSubscriber':{{width:50,hidden:true}},'Customer DeniedCustomer':{{width:50,hidden:true}},'Customer IsPartner':{{width:50,hidden:true}},'Customer BalancePages':{{width:50,hidden:true}},'Customer City':{{width:80,hidden:true}},'Customer Degree':{{width:50,hidden:true}},'Customer CountryStudy':{{width:80,hidden:true}},'Customer EnglishLevel':{{width:50,hidden:true}},'Customer RegDate':{{width:70,hidden:true}},'Customer LastLogin':{{width:70,hidden:true}},'Customer IsActive':{{width:50,hidden:true}},'Customer Country':{{width:80,hidden:true}},'Customer TimeZone':{{width:50,hidden:true}},'Customer PaymentDetails':{{width:80,hidden:true}},'Customer DisableNotifications':{{width:50,hidden:true}},'Customer Site':{{width:50,hidden:true}},'Customer FindUs':{{width:80,hidden:true}},'Customer UserType':{{width:80,hidden:true}},'Customer IsProfileEditing':{{width:50,hidden:true}},'Customer BirthDate':{{width:70,hidden:true}},'Customer IsFrozen':{{width:50,hidden:true}},'Writer History':{{width:80,hidden:true}},'Writer WorkStatus':{{width:80,hidden:true}},'Writer HireHistory':{{width:80,hidden:true}},'Writer Gender':{{width:80,hidden:true}},'Writer MaritalStatus':{{width:80,hidden:true}},'Writer TimeAvailableAtHomeFrom':{{width:50,hidden:true}},'Writer TimeAvailableAtHomeTo':{{width:50,hidden:true}},'Writer TimeAvailableAtWorkFrom':{{width:50,hidden:true}},'Writer TimeAvailableAtWorkTo':{{width:50,hidden:true}},'Writer TimeAvailableAtCellFrom':{{width:50,hidden:true}},'Writer TimeAvailableAtCellTo':{{width:50,hidden:true}},'Writer WorkHoursGrade':{{width:80,hidden:true}},'Writer Iam':{{width:80,hidden:true}},'Writer SalaryPeriodicity':{{width:80,hidden:true}},'Writer AssigmentsDone':{{width:50,hidden:true}},'Writer Sample':{{width:80,hidden:true}},'Writer WriteOther':{{width:50,hidden:true}},'Writer Enjoy':{{width:80,hidden:true}},'Writer Dislike':{{width:80,hidden:true}},'Writer WriterPaymentMethodId':{{width:80,hidden:true}},'Writer PaymentMethod1':{{width:80,hidden:true}},'Writer PaymentMethod2':{{width:80,hidden:true}},'Writer Employers':{{width:80,hidden:true}},'Writer HireReason':{{width:80,hidden:true}},'Writer Occupation':{{width:80,hidden:true}},'Writer Education':{{width:80,hidden:true}},'Writer Address':{{width:80,hidden:true}},'Writer City':{{width:80,hidden:true}},'Writer State':{{width:80,hidden:true}},'Writer StatusBalance':{{width:50,hidden:true}},'Writer StatusBalanceTmp':{{width:50,hidden:true}},'Writer Status':{{width:50,hidden:true}},'Writer ActivationDate':{{width:70,hidden:true}},'Writer DeactivationDate':{{width:70,hidden:true}},'Writer Status1stSetDate':{{width:70,hidden:true}},'Writer IsTracked':{{width:50,hidden:true}},'Writer CanSeePartners':{{width:50,hidden:true}},'Writer RegDate':{{width:70,hidden:true}},'Writer LastLogin':{{width:70,hidden:true}},'Writer IsActive':{{width:50,hidden:true}},'Writer Country':{{width:80,hidden:true}},'Writer TimeZone':{{width:50,hidden:true}},'Writer PaymentDetails':{{width:80,hidden:true}},'Writer DisableNotifications':{{width:50,hidden:true}},'Writer Site':{{width:50,hidden:true}},'Writer FindUs':{{width:80,hidden:true}},'Writer UserType':{{width:80,hidden:true}},'Writer IsProfileEditing':{{width:50,hidden:true}},'Writer BirthDate':{{width:70,hidden:true}},'Writer IsFrozen':{{width:50,hidden:true}}}}}},'test2':{{search:true,page:1,sortname:'',sortorder:'asc',permutation:[0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152],colStates:{{OrderId:{{width:50,hidden:true}},CustomerId:{{width:50,hidden:true}},WriterId:{{width:50,hidden:true}},PrefferedWriter:{{width:50,hidden:true}},StatusRaw:{{width:50,hidden:true}},'Major2 Name':{{width:80,hidden:true}},AssignDate:{{width:70,hidden:true}},DeletedDate:{{width:70,hidden:true}},CompletionDate:{{width:70,hidden:true}},DeadlineDate:{{width:70,hidden:true}},WriterDeadlineDate:{{width:70,hidden:true}},CreateDate:{{width:70,hidden:true}},AvailableDate:{{width:70,hidden:true}},LastRevisionDate:{{width:70,hidden:true}},PaymentDate:{{width:70,hidden:true}},DeadlineHours:{{width:50,hidden:true}},StudyLevel:{{width:80,hidden:true}},AssignmentType:{{width:80,hidden:true}},DeadlineHistory:{{width:80,hidden:true}},Revised:{{width:50,hidden:true}},RevCount:{{width:50,hidden:true}},PaymentStatus:{{width:80,hidden:true}},PaymentTrackingNumber:{{width:80,hidden:true}},PaymentChargeback:{{width:80,hidden:true}},PagePrice:{{width:50,hidden:true}},WriterPagePrice:{{width:50,hidden:true}},NumPages:{{width:50,hidden:true}},DiscountCode:{{width:80,hidden:true}},WriterBonus:{{width:50,hidden:true}},NumSources:{{width:50,hidden:true}},Subject:{{width:80,hidden:true}},Topic:{{width:80,hidden:true}},Books:{{width:80,hidden:true}},Description:{{width:70,hidden:true}},CitationStyle:{{width:80,hidden:true}},English:{{width:80,hidden:true}},NeedPlagiarismReport:{{width:50,hidden:true}},Spacing:{{width:80,hidden:true}},NativeLanguage:{{width:50,hidden:true}},TitlePage:{{width:50,hidden:true}},Outline:{{width:50,hidden:true}},Bibliography:{{width:50,hidden:true}},DeadlineFixed:{{width:50,hidden:true}},AdditionalMaterials:{{width:50,hidden:true}},TotalCost:{{width:50,hidden:true}},TotalWithDiscount:{{width:50,hidden:true}},DiscountTotal:{{width:50,hidden:true}},Currency:{{width:80,hidden:true}},IsSurveyFilled:{{width:50,hidden:true}},NotifiedLate:{{width:50,hidden:true}},ForcedlySkipPlagreport:{{width:50,hidden:true}},Risk:{{width:80,hidden:true}},WasWPPRaisedDueHighRisk:{{width:50,hidden:true}},LowPriceForHighRiskOrderWarnStatus:{{width:80,hidden:true}},OtherCitation:{{width:80,hidden:true}},CanSupportSeePartnerOrder:{{width:50,hidden:true}},CanPartnerSeeOrder:{{width:50,hidden:true}},NumberOfSlides:{{width:50,hidden:true}},WasOpenForEditBefore:{{width:50,hidden:true}},Abstract:{{width:50,hidden:true}},AssignedBy:{{width:50,hidden:true}},Archived:{{width:50,hidden:true}},CanExtendDeadline:{{width:50,hidden:true}},AnaliticScenario:{{width:80,hidden:true}},'Customer Employed':{{width:80,hidden:true}},'Customer MainMajors':{{width:80,hidden:true}},'Customer StudyLevel':{{width:80,hidden:true}},'Customer CurrentGPA':{{width:50,hidden:true}},'Customer DesiredGPA':{{width:50,hidden:true}},'Customer EnglishNative':{{width:50,hidden:true}},'Customer EnglishStudyYears':{{width:50,hidden:true}},'Customer Difficulties':{{width:80,hidden:true}},'Customer CommonMistakes':{{width:80,hidden:true}},'Customer CustomerQualityExpectations':{{width:80,hidden:true}},'Customer AdditionalComments':{{width:80,hidden:true}},'Customer IsWroteReview':{{width:50,hidden:true}},'Customer SendTips':{{width:50,hidden:true}},'Customer SendSeasonal':{{width:50,hidden:true}},'Customer NumCompletedOrders':{{width:50,hidden:true}},'Customer NumCompletedPages':{{width:50,hidden:true}},'Customer Bonus':{{width:50,hidden:true}},'Customer Emergency':{{width:50,hidden:true}},'Customer IsSubscriber':{{width:50,hidden:true}},'Customer DeniedCustomer':{{width:50,hidden:true}},'Customer IsPartner':{{width:50,hidden:true}},'Customer BalancePages':{{width:50,hidden:true}},'Customer City':{{width:80,hidden:true}},'Customer Degree':{{width:50,hidden:true}},'Customer CountryStudy':{{width:80,hidden:true}},'Customer EnglishLevel':{{width:50,hidden:true}},'Customer RegDate':{{width:70,hidden:true}},'Customer LastLogin':{{width:70,hidden:true}},'Customer IsActive':{{width:50,hidden:true}},'Customer Country':{{width:80,hidden:true}},'Customer TimeZone':{{width:50,hidden:true}},'Customer PaymentDetails':{{width:80,hidden:true}},'Customer DisableNotifications':{{width:50,hidden:true}},'Customer Site':{{width:50,hidden:true}},'Customer FindUs':{{width:80,hidden:true}},'Customer UserType':{{width:80,hidden:true}},'Customer IsProfileEditing':{{width:50,hidden:true}},'Customer BirthDate':{{width:70,hidden:true}},'Customer IsFrozen':{{width:50,hidden:true}},'Writer History':{{width:80,hidden:true}},'Writer WorkStatus':{{width:80,hidden:true}},'Writer HireHistory':{{width:80,hidden:true}},'Writer Gender':{{width:80,hidden:true}},'Writer MaritalStatus':{{width:80,hidden:true}},'Writer TimeAvailableAtHomeFrom':{{width:50,hidden:true}},'Writer TimeAvailableAtHomeTo':{{width:50,hidden:true}},'Writer TimeAvailableAtWorkFrom':{{width:50,hidden:true}},'Writer TimeAvailableAtWorkTo':{{width:50,hidden:true}},'Writer TimeAvailableAtCellFrom':{{width:50,hidden:true}},'Writer TimeAvailableAtCellTo':{{width:50,hidden:true}},'Writer WorkHoursGrade':{{width:80,hidden:true}},'Writer Iam':{{width:80,hidden:true}},'Writer SalaryPeriodicity':{{width:80,hidden:true}},'Writer AssigmentsDone':{{width:50,hidden:true}},'Writer Sample':{{width:80,hidden:true}},'Writer WriteOther':{{width:50,hidden:true}},'Writer Enjoy':{{width:80,hidden:true}},'Writer Dislike':{{width:80,hidden:true}},'Writer WriterPaymentMethodId':{{width:80,hidden:true}},'Writer PaymentMethod1':{{width:80,hidden:true}},'Writer PaymentMethod2':{{width:80,hidden:true}},'Writer Employers':{{width:80,hidden:true}},'Writer HireReason':{{width:80,hidden:true}},'Writer Occupation':{{width:80,hidden:true}},'Writer Education':{{width:80,hidden:true}},'Writer Address':{{width:80,hidden:true}},'Writer City':{{width:80,hidden:true}},'Writer State':{{width:80,hidden:true}},'Writer StatusBalance':{{width:50,hidden:true}},'Writer StatusBalanceTmp':{{width:50,hidden:true}},'Writer Status':{{width:50,hidden:true}},'Writer ActivationDate':{{width:70,hidden:true}},'Writer DeactivationDate':{{width:70,hidden:true}},'Writer Status1stSetDate':{{width:70,hidden:true}},'Writer IsTracked':{{width:50,hidden:true}},'Writer CanSeePartners':{{width:50,hidden:true}},'Writer RegDate':{{width:70,hidden:true}},'Writer LastLogin':{{width:70,hidden:true}},'Writer IsActive':{{width:50,hidden:true}},'Writer Country':{{width:80,hidden:true}},'Writer TimeZone':{{width:50,hidden:true}},'Writer PaymentDetails':{{width:80,hidden:true}},'Writer DisableNotifications':{{width:50,hidden:true}},'Writer Site':{{width:50,hidden:true}},'Writer FindUs':{{width:80,hidden:true}},'Writer UserType':{{width:80,hidden:true}},'Writer IsProfileEditing':{{width:50,hidden:true}},'Writer BirthDate':{{width:70,hidden:true}},'Writer IsFrozen':{{width:50,hidden:true}}}}}},'test3':{{search:true,page:1,sortname:'',sortorder:'asc',permutation:[0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152],colStates:{{OrderId:{{width:50,hidden:true}},CustomerId:{{width:50,hidden:true}},WriterId:{{width:50,hidden:true}},PrefferedWriter:{{width:50,hidden:true}},StatusRaw:{{width:50,hidden:true}},'Major2 Name':{{width:80,hidden:true}},AssignDate:{{width:70,hidden:true}},DeletedDate:{{width:70,hidden:true}},CompletionDate:{{width:70,hidden:true}},DeadlineDate:{{width:70,hidden:true}},WriterDeadlineDate:{{width:70,hidden:true}},CreateDate:{{width:70,hidden:true}},AvailableDate:{{width:70,hidden:true}},LastRevisionDate:{{width:70,hidden:true}},PaymentDate:{{width:70,hidden:true}},DeadlineHours:{{width:50,hidden:true}},StudyLevel:{{width:80,hidden:true}},AssignmentType:{{width:80,hidden:true}},DeadlineHistory:{{width:80,hidden:true}},Revised:{{width:50,hidden:true}},RevCount:{{width:50,hidden:true}},PaymentStatus:{{width:80,hidden:true}},PaymentTrackingNumber:{{width:80,hidden:true}},PaymentChargeback:{{width:80,hidden:true}},PagePrice:{{width:50,hidden:true}},WriterPagePrice:{{width:50,hidden:true}},NumPages:{{width:50,hidden:true}},DiscountCode:{{width:80,hidden:true}},WriterBonus:{{width:50,hidden:true}},NumSources:{{width:50,hidden:true}},Subject:{{width:80,hidden:true}},Topic:{{width:80,hidden:true}},Books:{{width:80,hidden:true}},Description:{{width:70,hidden:true}},CitationStyle:{{width:80,hidden:true}},English:{{width:80,hidden:true}},NeedPlagiarismReport:{{width:50,hidden:true}},Spacing:{{width:80,hidden:true}},NativeLanguage:{{width:50,hidden:true}},TitlePage:{{width:50,hidden:true}},Outline:{{width:50,hidden:true}},Bibliography:{{width:50,hidden:true}},DeadlineFixed:{{width:50,hidden:true}},AdditionalMaterials:{{width:50,hidden:true}},TotalCost:{{width:50,hidden:true}},TotalWithDiscount:{{width:50,hidden:true}},DiscountTotal:{{width:50,hidden:true}},Currency:{{width:80,hidden:true}},IsSurveyFilled:{{width:50,hidden:true}},NotifiedLate:{{width:50,hidden:true}},ForcedlySkipPlagreport:{{width:50,hidden:true}},Risk:{{width:80,hidden:true}},WasWPPRaisedDueHighRisk:{{width:50,hidden:true}},LowPriceForHighRiskOrderWarnStatus:{{width:80,hidden:true}},OtherCitation:{{width:80,hidden:true}},CanSupportSeePartnerOrder:{{width:50,hidden:true}},CanPartnerSeeOrder:{{width:50,hidden:true}},NumberOfSlides:{{width:50,hidden:true}},WasOpenForEditBefore:{{width:50,hidden:true}},Abstract:{{width:50,hidden:true}},AssignedBy:{{width:50,hidden:true}},Archived:{{width:50,hidden:true}},CanExtendDeadline:{{width:50,hidden:true}},AnaliticScenario:{{width:80,hidden:true}},'Customer Employed':{{width:80,hidden:true}},'Customer MainMajors':{{width:80,hidden:true}},'Customer StudyLevel':{{width:80,hidden:true}},'Customer CurrentGPA':{{width:50,hidden:true}},'Customer DesiredGPA':{{width:50,hidden:true}},'Customer EnglishNative':{{width:50,hidden:true}},'Customer EnglishStudyYears':{{width:50,hidden:true}},'Customer Difficulties':{{width:80,hidden:true}},'Customer CommonMistakes':{{width:80,hidden:true}},'Customer CustomerQualityExpectations':{{width:80,hidden:true}},'Customer AdditionalComments':{{width:80,hidden:true}},'Customer IsWroteReview':{{width:50,hidden:true}},'Customer SendTips':{{width:50,hidden:true}},'Customer SendSeasonal':{{width:50,hidden:true}},'Customer NumCompletedOrders':{{width:50,hidden:true}},'Customer NumCompletedPages':{{width:50,hidden:true}},'Customer Bonus':{{width:50,hidden:true}},'Customer Emergency':{{width:50,hidden:true}},'Customer IsSubscriber':{{width:50,hidden:true}},'Customer DeniedCustomer':{{width:50,hidden:true}},'Customer IsPartner':{{width:50,hidden:true}},'Customer BalancePages':{{width:50,hidden:true}},'Customer City':{{width:80,hidden:true}},'Customer Degree':{{width:50,hidden:true}},'Customer CountryStudy':{{width:80,hidden:true}},'Customer EnglishLevel':{{width:50,hidden:true}},'Customer RegDate':{{width:70,hidden:true}},'Customer LastLogin':{{width:70,hidden:true}},'Customer IsActive':{{width:50,hidden:true}},'Customer Country':{{width:80,hidden:true}},'Customer TimeZone':{{width:50,hidden:true}},'Customer PaymentDetails':{{width:80,hidden:true}},'Customer DisableNotifications':{{width:50,hidden:true}},'Customer Site':{{width:50,hidden:true}},'Customer FindUs':{{width:80,hidden:true}},'Customer UserType':{{width:80,hidden:true}},'Customer IsProfileEditing':{{width:50,hidden:true}},'Customer BirthDate':{{width:70,hidden:true}},'Customer IsFrozen':{{width:50,hidden:true}},'Writer History':{{width:80,hidden:true}},'Writer WorkStatus':{{width:80,hidden:true}},'Writer HireHistory':{{width:80,hidden:true}},'Writer Gender':{{width:80,hidden:true}},'Writer MaritalStatus':{{width:80,hidden:true}},'Writer TimeAvailableAtHomeFrom':{{width:50,hidden:true}},'Writer TimeAvailableAtHomeTo':{{width:50,hidden:true}},'Writer TimeAvailableAtWorkFrom':{{width:50,hidden:true}},'Writer TimeAvailableAtWorkTo':{{width:50,hidden:true}},'Writer TimeAvailableAtCellFrom':{{width:50,hidden:true}},'Writer TimeAvailableAtCellTo':{{width:50,hidden:true}},'Writer WorkHoursGrade':{{width:80,hidden:true}},'Writer Iam':{{width:80,hidden:true}},'Writer SalaryPeriodicity':{{width:80,hidden:true}},'Writer AssigmentsDone':{{width:50,hidden:true}},'Writer Sample':{{width:80,hidden:true}},'Writer WriteOther':{{width:50,hidden:true}},'Writer Enjoy':{{width:80,hidden:true}},'Writer Dislike':{{width:80,hidden:true}},'Writer WriterPaymentMethodId':{{width:80,hidden:true}},'Writer PaymentMethod1':{{width:80,hidden:true}},'Writer PaymentMethod2':{{width:80,hidden:true}},'Writer Employers':{{width:80,hidden:true}},'Writer HireReason':{{width:80,hidden:true}},'Writer Occupation':{{width:80,hidden:true}},'Writer Education':{{width:80,hidden:true}},'Writer Address':{{width:80,hidden:true}},'Writer City':{{width:80,hidden:true}},'Writer State':{{width:80,hidden:true}},'Writer StatusBalance':{{width:50,hidden:true}},'Writer StatusBalanceTmp':{{width:50,hidden:true}},'Writer Status':{{width:50,hidden:true}},'Writer ActivationDate':{{width:70,hidden:true}},'Writer DeactivationDate':{{width:70,hidden:true}},'Writer Status1stSetDate':{{width:70,hidden:true}},'Writer IsTracked':{{width:50,hidden:true}},'Writer CanSeePartners':{{width:50,hidden:true}},'Writer RegDate':{{width:70,hidden:true}},'Writer LastLogin':{{width:70,hidden:true}},'Writer IsActive':{{width:50,hidden:true}},'Writer Country':{{width:80,hidden:true}},'Writer TimeZone':{{width:50,hidden:true}},'Writer PaymentDetails':{{width:80,hidden:true}},'Writer DisableNotifications':{{width:50,hidden:true}},'Writer Site':{{width:50,hidden:true}},'Writer FindUs':{{width:80,hidden:true}},'Writer UserType':{{width:80,hidden:true}},'Writer IsProfileEditing':{{width:50,hidden:true}},'Writer BirthDate':{{width:70,hidden:true}},'Writer IsFrozen':{{width:50,hidden:true}}}}}}}};");
            htmlBuilder.AppendFormat(@"var templates={{");
            foreach (var templ in model.Templates)
            {
                htmlBuilder.AppendFormat(@"'{0}':{1}, ", templ.Name, templ.Content);
            }
            htmlBuilder.AppendFormat(@"}}; ");
            //htmlBuilder.AppendFormat(@"console.log(templates); ");
            htmlBuilder.AppendFormat(@"}});");
            htmlBuilder.AppendFormat(@"</script>");


            return MvcHtmlString.Create(htmlBuilder.ToString());
        }

        public static string ReplaceEntity(string name)
        {
            return name.Replace("Customer_", "").Replace("Writer_", "");
        }
    }
}