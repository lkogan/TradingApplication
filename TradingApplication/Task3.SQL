-- You have below structure describing activity of clients on company's website:
-- client_activity
-- --------------------------
-- `id` bigint(20) NOT NULL AUTO_INCREMENT
-- `uri` varchar(4000) NOT NULL
-- `duration_ms` int(11) NOT NULL DEFAULT '0'
-- `timestamp` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
-- Sample data
-- ----------------
-- 1, https://somewebsite.com/WebPage1, 253, 2016-08-16 23:51:00.482000
-- 2, https://somewebsite.com/WebPage2, 57, 2016-08-16 23:51:00.346000
-- 3, https://somewebsite.com/WebPage3, 344, 2016-08-16 23:51:00.789000
-- ...
-- Create SQL code to calculate 95th percentile of requests duration over some date range
-- for each distinct URI in the activity log.
 
-- SOLUTION --
select distinct uri 
       ,PERCENTILE_DISC(0.5) WITHIN GROUP (ORDER BY duration_ms)  
        OVER (PARTITION BY uri) AS duration   
        from client_activity
        WHERE timestamp BETWEEN @StartDate AND @EndDate
		ORDER by uri ASC