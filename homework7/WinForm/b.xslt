<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:template match="/ArrayOfOrder">
		<html>
			<head>
				<title>Orders</title>
			</head>
			<body>
				<ul>
					<xsl:for-each select="Order">
						<li>														
							<em>ID:   </em><xsl:value-of select="ID" /><br/>
							<em>Username:  </em><xsl:value-of select="Username" /><br/>
							<em>Phone:  </em><xsl:value-of select="Phone" /><br/>
							<xsl:for-each select="Details/OrderDetails">
								<li><em>Details: <br/></em>
									<em>Name:  </em><xsl:value-of select="Name" /><br/>
									<em>Price:  </em><xsl:value-of select="Price" /><br/>
									<em>Account:  </em><xsl:value-of select="Account" /><br/>
								</li>
							</xsl:for-each>
						</li><br/><br/>
					</xsl:for-each>
				</ul>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>