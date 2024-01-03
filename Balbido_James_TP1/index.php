<html lang = "en">
	<head>
		<meta charset = "UTF +8">
		<meta http-equiv="X-UA-Compatible" content = "IE=edge">
		<meta name = "viewport" content = "width = device-width, initial-scale=1.0">
		<title>Website Portfolio</title>
		<link rel= "stylesheet" href= "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.0/css/all.min.css">
		<link rel="stylesheet" href = "style.css">
	</head>

	<?php 
		$name = "James Balbido";
		$course = "BSCS Student"
		
	?>
	 
	<body>
		<div class = "header">
			<nav>
				<a href = "#" class = "logo"><?php echo "$name"?> </a>
				<div class = "nav-links">
					<ul>
						<li><a href = "#"> Home </a></li>
						<li><a href = "#"> Services </a></li>
						<li><a href = "#"> Portfolio </a></li>
						<li><a href = "#"> About </a></li>
						<li><a href = "#"> Connect </a></li>
					</ul>
			</nav>
			<div class = "left-sidebar"></div>
			<div class = "row">
				<div class = "left-col">
				<img src = "fort.jpg" />
			</div>
			<div class = "right-col">
				<h1> I'm <?php echo $name ?> <br>
				<?php echo $course ?>
				</h1>
				<p>I am pursuing the computer science career because of how I'm fond on working about the technical stuff of 
				computers </p>
					<div class = "social-media">
						<div class = "icon">
							<i class = "fab fa-facebook-f"></i>
							<a href = "#">
								<i class = "fas fa-external-link-alt"></i>
							</a>
						</div>
						<div class = "icon">
							<i class = "fab fa-twitter"></i>
							<a href = "#">
								<i class = "fas fa-external-link-alt"></i>
							</a>
						</div>	
						<div class = "icon">
							<i class = "fab fa-youtube"></i>
							<a href = "#">
								<i class = "fas fa-external-link-alt"></i>
							</a>
						</div>
						<div class = "icon">
							<i class = "fab fa-instagram"></i>
							<a href = "#">
								<i class = "fas fa-external-link-alt"></i>
							</a>
						</div>
					</div>
			</div>
		</div>
</div>
	</body>
</html>