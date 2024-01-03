<html>
    <head>
        <title>Midterm</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
		<link rel="stylesheet" href="style.css"/>
    </head>
    <body>
	<!-- php code here-->
	
	<?php
	
		$FirstName = "James";
		$LastName = "Balbido";
		//Programming1
		$Programming1Title = "Web Programming";
		$Programming1Logo = "html.png";
		//Programming2
		$Programming2Title = "Mobile Programming";
		$Programming2Logo = "android.png";
		//Programming3
		$Programming3Title = "Desktop Programming";
		$Programming3Logo = "java.png";

		$menu = array ("Home", "Subjects", "Assignment", "Quiz", "About");
		$prog1 = array("HTML, CSS, and JavaScript", "PHP", "ASP.NET");
		$prog2 = array("Android", "IOS", "Windows");
		$prog3 = array("Java", "C#", "Python");
	

	?>
    <div class="row header-content">
		<div class="column-12">
			<?php echo "$FirstName ". "$LastName"?>
		</div>
	</div> 
		<!-- end of div: header -->
		<div class="row">
			<div class="column-3 border-profile">
				<div class="row">
					<div class="column-12">
						<img src="login_logo.png"/>
					</div>
				</div>
				<div class="row">
					<div class="column-12">
						<p>Name: <?php echo "$FirstName ". "$LastName"?></p>
					</div>
				</div>
				<div class="row">
					<div class="col-12 menu">
						<ul>
							
								<?php foreach ($menu as $value){
									echo "<li>$value <br/></li>";
								}?>
							
						</ul>
					</div>
				</div>
				<!-- end of row: menu -->
			</div> 
			<!-- end of row: profile -->
			<!-- 2nd Column: Programming Subjects -->
			<div class="column-9">
				<div class="row">
					<div class="column-4 menu">
						<div class="border-subjects">
							<div class="row">
								<div class="column-12">
									<img src="<?php echo "$Programming1Logo"?>"/>
								</div>
							</div>
							<div class="row">
								<div class="column-12">
									<ul>	
										<li class="subject">
											<?php echo "$Programming1Title";?>
										</li>
										<?php foreach ($prog1 as $value){
										echo "<li>$value <br/></li>";
										}?>
									</ul>
								</div>
							</div>
						</div>
					</div>
					<div class="column-4 menu">
						<div class="border-subjects">
							<div class="row">
								<div class="column-12">
									<img id="img-android" src="<?php echo "$Programming2Logo";?>"/>
								</div>
							</div>
							<div class="row">
								<div class="column-12">
									<ul>	
										<li class="subject">
											<?php echo "$Programming2Title";?>
										</li>
										<?php foreach ($prog2 as $value){
										echo "<li>$value <br/></li>";
										}?>
									</ul>
								</div>
							</div>
						</div>
					</div>
					<div class="column-4 menu">
						<div class="border-subjects">
							<div class="row">
								<div class="column-12">
									<img src="<?php echo "$Programming3Logo";?>"/>
								</div>
							</div>
							<div class="row">
								<div class="column-12">
									<ul>	
										<li class="subject">
											<?php echo "$Programming3Title";?>
										</li>
										<?php foreach ($prog3 as $value){
										echo "<li>$value <br/></li>";
										}?>
									</ul>
								</div>
							</div>
						</div>
					</div>
				</div><!-- end of row -->
			</div>
		</div>
    </body>
</html>