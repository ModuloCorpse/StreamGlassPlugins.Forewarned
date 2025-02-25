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
		var evidenceElement = document.getElementById(data);
		//var ruleOutElement = evidenceElement.getElementsByClassName("rule_out_img");
		if (evidenceElement.style.display === 'inline-block')
		{
			//if (ruleOutElement.style.display === 'inline-block')
			//	ruleOutElement.style.display = 'none';
			//else
				evidenceElement.style.display = 'none';
		}
		else
		{
			evidenceElement.style.display = 'inline-block';
			//ruleOutElement.style.display = 'none';
		}
	}

	#OnRuledOutEvidences(data)
	{
		var evidenceElement = document.getElementById(data);
		var ruleOutElement = evidenceElement.getElementsByClassName("rule_out_img");
		if (evidenceElement.style.display === 'inline-block')
		{
			if (ruleOutElement.style.display === 'none')
				ruleOutElement.style.display = 'inline-block';
			else
				evidenceElement.style.display = 'none';
		}
		else
		{
			evidenceElement.style.display = 'inline-block';
			ruleOutElement.style.display = 'inline-block';
		}
	}

	#OnLoadEvidences(msg)
	{
		const data = JSON.parse(msg);
		if (data.hasOwnProperty('evidences'))
		{
			var evidences = data['evidences'];
			for (var i = 0; i < evidences.length; i++)
				this.#OnEvidences(evidences[i]);
		}
		super.UnholdEvents();
	}

	Init()
	{
		super.Get('/forewarned/evidences', this.#OnLoadEvidences.bind(this));
		super.HoldEvents();
		super.RegisterToEvent('forewarned_evidences', this.#OnEvidences.bind(this));
		super.RegisterToEvent('forewarned_reset', this.#OnReset.bind(this));
		//super.RegisterToEvent('forewarned_rule_out', this.#OnRuledOutEvidences.bind(this));
	}
}

var streamGlassModuleClient;

function OnLoad()
{
	streamGlassModuleClient = new ForewarnedModule();
}