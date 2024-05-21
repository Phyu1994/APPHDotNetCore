namespace APPHDotNetCore.PizzaApi.Queries
{
    public class PizzaQuery
    {
        public static string PizzaOrderQuery { get; } =
            @"Select po.*, p.Pizza,p.Price from Tbl_PizzaOrder po 
            inner join Tbl_Pizza p on po.PizzaId = p.PizzaId
            where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";

        public static string PizzaOrderDetailQuery { get; } =
            @"Select pod.*,pe.Pizza,pe.price from Tbl_PizzaOrderDetail pod
            inner join Tbl_Pizza pe on pod.PizzaOrderDetailId = pe.PizzaId
            where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";
    }
}
