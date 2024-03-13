class ForewarnedModule extends StreamGlassEventWebsocket
{
	#OnReset(_event)
	{
		var slides = document.getElementsByClassName("evidence_div");
		for (var i = 0; i < slides.length; i++)
   			slides.item(i).style.display = 'none';
	}
	
	#OnEvidences(data)
	{
		if (data.hasOwnProperty('evidence'))
		{
			var evidence = data['evidence'];
			var evidenceElement = document.getElementById(evidence);
			if (evidenceElement.style.display === 'inline-block')
				evidenceElement.style.display = 'none';
			else
				evidenceElement.style.display = 'inline-block';
		}
	}

	Init()
	{
		super.RegisterToEvent('forewarned_evidences', this.#OnEvidences.bind(this));
		super.RegisterToEvent('forewarned_reset', this.#OnReset.bind(this));
	}
}

var streamGlassModuleClient;

function OnLoad()
{
	streamGlassModuleClient = new ForewarnedModule();
}