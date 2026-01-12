CREATE OR REPLACE VIEW vwGetIncomesAndExpenses AS
SELECT
    tran.userId AS userId,
    MONTH(tran.paidOrReceivedAt) AS month,
    YEAR(tran.paidOrReceivedAt) AS year,
    SUM(CASE WHEN tran.type = 1 THEN tran.amount ELSE 0 END) AS incomes,
    SUM(CASE WHEN tran.type = 2 THEN tran.amount ELSE 0 END) AS expenses
FROM
    `transaction` tran
WHERE
    tran.paidOrReceivedAt >= CURDATE() - INTERVAL 11 MONTH
  AND tran.paidOrReceivedAt <= CURDATE() + INTERVAL 1 MONTH
GROUP BY
    tran.userId,
    MONTH(tran.paidOrReceivedAt),
    YEAR(tran.paidOrReceivedAt);
