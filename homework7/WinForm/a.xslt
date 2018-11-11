<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:template match="/">
		<html>
			<head>
				<title>Orders</title>
			</head>
			<body>
				<ul>										
						<xsl:for-each select="Orderdetails">
							<li>
								<xsl:value-of select="." /><br/>
							</li>
						</xsl:for-each>					
				</ul>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>