// 篩選
$(function(){
    // select
    $(".filter-title").on("click", function(e){
        $(".filter-select").slideToggle();
        $(".filter-select").toggleClass("is-open");
        event.stopPropagation();
    });

    // checkbox
    $(".filter-option input:checkbox").on("change", function(e){
        let val = $(this).val();
        $(this).toggleClass("is-selected");
        $(".filter-labels").find("[data-id=" + val + "]").toggleClass("is-selected");

        // 項目篩選
        $(".filter-item").addClass("is-hide");
        $(".filter-item[data-id=" + val + "]").toggleClass("is-selected");

        // 計算篩選項目數量顯示或隱藏該系統
        $(".index-column-box").each(function(){
            let x = $(this).find(".filter-item.is-selected").length;
            if(x > 0) {
                $(this).removeClass("is-close");
            }
            else {
                $(this).addClass("is-close");
            }
        });

        // 運算標籤選取的數量顯示或隱藏清除按鈕
        let n = $(".filter-label.is-selected").length;
        if(n > 0) {
            $(".filter-clean").addClass("is-show");
        }
        else {
            $(".filter-clean").removeClass("is-show");
            $(".filter-item").removeClass("is-hide is-selected");
            $(".index-column-box").removeClass("is-close");
        }
    });

    // 刪除標籤
    $(".filter-del").on("click", function(e){
        let val = $(this).parent().data("id");
        $("#" + val).prop("checked", false);
        $(this).parent().removeClass("is-selected");
        $(".filter-item[data-id=" + val + "]").removeClass("is-selected");
        let n = $(".filter-label.is-selected").length;
        if(n < 1){
            $(".filter-clean").removeClass("is-show");
            $(".filter-item").removeClass("is-hide is-selected");
            $(".index-column-box").removeClass("is-close");
        }
        $(".filter-select").slideUp();
    });

    // 清除
    $(".filter-clean").on("click", function(e){
        $(this).removeClass("is-show");
        $(".filter-option input:checkbox").prop("checked", false);
        $(".filter-label").removeClass("is-selected");
        $(".filter-select").slideUp();
        $(".filter-item").removeClass("is-hide is-selected");
        $(".index-column-box").removeClass("is-close");
    });

    $(".index-column-wrap").click(function(){
        $(".filter-select").slideUp();
    });
});


// 選擇系統 彈窗
$(function(){
    // 開啟
    $("#filterAdd").on("click", function(e){
        $("#demo1").addClass("is-active");
        $(".popup-overlay").addClass("is-active");
        $(".filter-select").slideUp();
    });

    // 清除條件
    $("#filterClean").on("click", function(e){
        $(".p-sys-item input:checkbox").prop("checked", false);
        $(".filter-group").removeClass("is-hide is-selected");
        $(".top-item").removeClass("is-hide is-selected");
        $(".index-column-box").removeClass("is-hide is-selected");
    });

    // 選擇系統篩選判斷
    $(".p-sys-item input:checkbox").on("change", function(e){
        let val = $(this).val();
        if($(this).is(":checked")) {
            $("#g" + val).addClass("is-selected");
            $("#t" + val).addClass("is-selected");
            $("#c" + val).addClass("is-selected");
        }
        else {
            $("#g" + val).removeClass("is-selected");
            $("#t" + val).removeClass("is-selected");
            $("#c" + val).removeClass("is-selected");
        }
    });

    // 確定
    $("#filterConfirm").on("click", function(e){
        $(".popup-wrap").removeClass("is-active");
        $(".popup-overlay").removeClass("is-active");
        let n = $(".top-item.is-selected").length;
        if(n > 0) {
            $(".filter-group").addClass("is-hide");
            $(".top-item").addClass("is-hide");
            $(".index-column-box").addClass("is-hide");
        }
        else {
            $(".filter-group").removeClass("is-hide");
            $(".top-item").removeClass("is-hide");
            $(".index-column-box").removeClass("is-hide");
        }
    });
});