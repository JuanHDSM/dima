CREATE OR REPLACE VIEW vwgetassetsinwallet AS
SELECT 
    `a`.`userId` AS `userId`,
    `a`.`logoUrl` AS `logoUrl`,
    `a`.`symbol` AS `symbol`,
    `a`.`regularMarketPrice` AS `currentPrice`,
    COUNT(`a`.`symbol`) AS `quantity`,
    AVG(`a`.`regularMarketPrice`) AS `average`,
    SUM(`a`.`regularMarketPrice`) AS `balance`
FROM 
    `stocks` `a`
GROUP BY 
    `a`.`userId`, `a`.`symbol`, `a`.`logoUrl`, `a`.`regularMarketPrice`;
