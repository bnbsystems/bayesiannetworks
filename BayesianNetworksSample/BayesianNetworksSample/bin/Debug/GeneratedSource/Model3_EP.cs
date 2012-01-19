using System;
using MicrosoftResearch.Infer;
using MicrosoftResearch.Infer.Distributions;
using MicrosoftResearch.Infer.Collections;
using MicrosoftResearch.Infer.Factors;

namespace MicrosoftResearch.Infer.Models.User
{
	/// <summary>
	/// Class for performing inference in model 'Model' using algorithm 'ExpectationPropagation'.
	/// </summary>
	/// <remarks>
	/// The easiest way to use this class is to wrap an instance in a CompiledAlgorithm object and use
	/// the methods on CompiledAlgorithm to set parameters and execute inference.
	/// 
	/// If you instead wish to use this class directly, you must perform the following steps:
	/// 1) Create an instance of the class
	/// 2) Set the value of any externally-set fields e.g. data, priors
	/// 3) Call the Execute(numberOfIterations) method
	/// 4) Use the XXXMarginal() methods to retrieve posterior marginals for different variables.
	/// 
	/// Generated by Infer.NET 2.4 beta 3 at 16:28 on 4 stycznia 2012.
	/// </remarks>
	public class Model3_EP : IGeneratedAlgorithm
	{
		#region Fields
		/// <summary>Field backing the NumberOfIterationsDone property</summary>
		private int numberOfIterationsDone;
		/// <summary>Field backing the WetGrass property</summary>
		private bool wetGrass;
		/// <summary>The number of iterations last computed by Changed_numberOfIterationsDecreased_WetGrass. Set this to zero to force re-execution of Changed_numberOfIterationsDecreased_WetGrass</summary>
		public int Changed_numberOfIterationsDecreased_WetGrass_iterationsDone;
		/// <summary>The number of iterations last computed by Constant. Set this to zero to force re-execution of Constant</summary>
		public int Constant_iterationsDone;
		/// <summary>The number of iterations last computed by Init_numberOfIterationsDecreased_WetGrass. Set this to zero to force re-execution of Init_numberOfIterationsDecreased_WetGrass</summary>
		public int Init_numberOfIterationsDecreased_WetGrass_iterationsDone;
		/// <summary>True if Init_numberOfIterationsDecreased_WetGrass has performed initialisation. Set this to false to force re-execution of Init_numberOfIterationsDecreased_WetGrass</summary>
		public bool Init_numberOfIterationsDecreased_WetGrass_isInitialised;
		/// <summary>The constant 'vBernoulli21'</summary>
		public Bernoulli vBernoulli21;
		public DistributionStructArray<Bernoulli,bool>[] Cloudy_selector_cases_uses_B;
		/// <summary>Message from use of 'Rain'</summary>
		public Bernoulli Rain_use_B;
		public Bernoulli WetGrass_marginal;
		/// <summary>Message to marginal of 'Cloudy'</summary>
		public Bernoulli Cloudy_marginal_F;
		/// <summary>Message to marginal of 'Rain'</summary>
		public Bernoulli Rain_marginal_F;
		/// <summary>Message to marginal of 'Sprinkler'</summary>
		public Bernoulli Sprinkler_marginal_F;
		#endregion

		#region Properties
		/// <summary>The number of iterations done from the initial state</summary>
		public int NumberOfIterationsDone
		{			get {
				return this.numberOfIterationsDone;
			}
		}

		/// <summary>The externally-specified value of 'WetGrass'</summary>
		public bool WetGrass
		{			get {
				return this.wetGrass;
			}
			set {
				if (this.wetGrass!=value) {
					this.wetGrass = value;
					this.numberOfIterationsDone = 0;
					this.Init_numberOfIterationsDecreased_WetGrass_isInitialised = false;
					this.Changed_numberOfIterationsDecreased_WetGrass_iterationsDone = 0;
				}
			}
		}

		#endregion

		#region Methods
		/// <summary>Get the observed value of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		public object GetObservedValue(string variableName)
		{
			if (variableName=="WetGrass") {
				return this.WetGrass;
			}
			throw new ArgumentException("Not an observed variable name: "+variableName);
		}

		/// <summary>Set the observed value of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Observed value</param>
		public void SetObservedValue(string variableName, object value)
		{
			if (variableName=="WetGrass") {
				this.WetGrass = (bool)value;
				return ;
			}
			throw new ArgumentException("Not an observed variable name: "+variableName);
		}

		/// <summary>The marginal distribution of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		public object Marginal(string variableName)
		{
			if (variableName=="Cloudy") {
				return this.CloudyMarginal();
			}
			if (variableName=="Rain") {
				return this.RainMarginal();
			}
			if (variableName=="Sprinkler") {
				return this.SprinklerMarginal();
			}
			if (variableName=="WetGrass") {
				return this.WetGrassMarginal();
			}
			throw new ArgumentException("This class was not built to infer "+variableName);
		}

		public T Marginal<T>(string variableName)
		{
			return Distribution.ChangeType<T>(this.Marginal(variableName));
		}

		/// <summary>The query-specific marginal distribution of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="query">QueryType name. For example, GibbsSampling answers 'Marginal', 'Samples', and 'Conditionals' queries</param>
		public object Marginal(string variableName, string query)
		{
			if (query=="Marginal") {
				return this.Marginal(variableName);
			}
			throw new ArgumentException(((("This class was not built to infer \'"+variableName)+"\' with query \'")+query)+"\'");
		}

		public T Marginal<T>(string variableName, string query)
		{
			return Distribution.ChangeType<T>(this.Marginal(variableName, query));
		}

		/// <summary>The output message of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		public object GetOutputMessage(string variableName)
		{
			throw new ArgumentException("This class was not built to compute an output message for "+variableName);
		}

		/// <summary>Update all marginals, by iterating message passing the given number of times</summary>
		/// <param name="numberOfIterations">The number of times to iterate each loop</param>
		/// <param name="initialise">If true, messages that initialise loops are reset when observed values change</param>
		private void Execute(int numberOfIterations, bool initialise)
		{
			if (numberOfIterations<this.Changed_numberOfIterationsDecreased_WetGrass_iterationsDone) {
				this.Init_numberOfIterationsDecreased_WetGrass_isInitialised = false;
				this.Changed_numberOfIterationsDecreased_WetGrass_iterationsDone = 0;
			}
			this.Constant();
			this.Init_numberOfIterationsDecreased_WetGrass(initialise);
			this.Changed_numberOfIterationsDecreased_WetGrass(numberOfIterations);
			this.numberOfIterationsDone = numberOfIterations;
		}

		public void Execute(int numberOfIterations)
		{
			this.Execute(numberOfIterations, true);
		}

		public void Update(int additionalIterations)
		{
			this.Execute(this.numberOfIterationsDone+additionalIterations, false);
		}

		private void OnProgressChanged(ProgressChangedEventArgs e)
		{
			// Make a temporary copy of the event to avoid a race condition
			// if the last subscriber unsubscribes immediately after the null check and before the event is raised.
			EventHandler<ProgressChangedEventArgs> handler = this.ProgressChanged;
			if (handler!=null) {
				handler(this, e);
			}
		}

		/// <summary>Reset all messages to their initial values.  Sets NumberOfIterationsDone to 0.</summary>
		public void Reset()
		{
			this.Execute(0);
		}

		/// <summary>Computations that do not depend on observed values</summary>
		public void Constant()
		{
			if (this.Constant_iterationsDone==1) {
				return ;
			}
			this.vBernoulli21 = Bernoulli.FromLogOdds(1.3862943611198908);
			// Create array for 'Cloudy_selector_cases_uses' Backwards messages.
			this.Cloudy_selector_cases_uses_B = new DistributionStructArray<Bernoulli,bool>[4];
			for(int _ind = 0; _ind<4; _ind++) {
				// Create array for 'Cloudy_selector_cases_uses' Backwards messages.
				this.Cloudy_selector_cases_uses_B[_ind] = new DistributionStructArray<Bernoulli,bool>(2);
			}
			this.Constant_iterationsDone = 1;
			this.Changed_numberOfIterationsDecreased_WetGrass_iterationsDone = 0;
			this.Init_numberOfIterationsDecreased_WetGrass_iterationsDone = 0;
		}

		/// <summary>Computations that must reset on changes to numberOfIterationsDecreased and WetGrass</summary>
		/// <param name="initialise">If true, reset messages that initialise loops</param>
		public void Init_numberOfIterationsDecreased_WetGrass(bool initialise)
		{
			if ((this.Init_numberOfIterationsDecreased_WetGrass_iterationsDone==1)&&((!initialise)||this.Init_numberOfIterationsDecreased_WetGrass_isInitialised)) {
				return ;
			}
			for(int _ind = 0; _ind<4; _ind++) {
				for(int _iv0 = 0; _iv0<2; _iv0++) {
					this.Cloudy_selector_cases_uses_B[_ind][_iv0] = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
				}
			}
			this.Rain_use_B = ArrayHelper.MakeUniform<Bernoulli>(this.vBernoulli21);
			this.Init_numberOfIterationsDecreased_WetGrass_iterationsDone = 1;
			this.Init_numberOfIterationsDecreased_WetGrass_isInitialised = true;
			this.Changed_numberOfIterationsDecreased_WetGrass_iterationsDone = 0;
		}

		/// <summary>Computations that depend on the observed value of numberOfIterationsDecreased and WetGrass</summary>
		/// <param name="numberOfIterations">The number of times to iterate each loop</param>
		public void Changed_numberOfIterationsDecreased_WetGrass(int numberOfIterations)
		{
			if (this.Changed_numberOfIterationsDecreased_WetGrass_iterationsDone==numberOfIterations) {
				return ;
			}
			// The constant 'vBernoulli23'
			Bernoulli vBernoulli23 = Bernoulli.FromLogOdds(4.5951198501345889);
			this.WetGrass_marginal = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli23);
			this.WetGrass_marginal = Distribution.SetPoint<Bernoulli,bool>(this.WetGrass_marginal, this.wetGrass);
			Bernoulli Rain_F = ArrayHelper.MakeUniform<Bernoulli>(this.vBernoulli21);
			// The constant 'vBernoulli18'
			Bernoulli vBernoulli18 = Bernoulli.FromLogOdds(0);
			this.Cloudy_marginal_F = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli18);
			DistributionStructArray<Bernoulli,bool> Rain_cond_Cloudy_F = default(DistributionStructArray<Bernoulli,bool>);
			// Create array for 'Rain_cond_Cloudy' Forwards messages.
			Rain_cond_Cloudy_F = new DistributionStructArray<Bernoulli,bool>(2);
			for(int _gateind = 0; _gateind<2; _gateind++) {
				Rain_cond_Cloudy_F[_gateind] = ArrayHelper.MakeUniform<Bernoulli>(this.vBernoulli21);
			}
			// Message to 'Rain_cond_Cloudy' from Copy factor
			Rain_cond_Cloudy_F[0] = Factor.Copy<Bernoulli>(this.vBernoulli21);
			// The constant 'vBernoulli22'
			Bernoulli vBernoulli22 = Bernoulli.FromLogOdds(0.28185115214098749);
			// Message to 'Rain_cond_Cloudy' from Copy factor
			Rain_cond_Cloudy_F[1] = Factor.Copy<Bernoulli>(vBernoulli22);
			DistributionStructArray<Bernoulli,bool> Cloudy_selector_cases_F = default(DistributionStructArray<Bernoulli,bool>);
			// Create array for 'Cloudy_selector_cases' Forwards messages.
			Cloudy_selector_cases_F = new DistributionStructArray<Bernoulli,bool>(2);
			for(int _iv0 = 0; _iv0<2; _iv0++) {
				Cloudy_selector_cases_F[_iv0] = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
			}
			// Message to 'Cloudy_selector_cases' from Cases factor
			Cloudy_selector_cases_F = CasesOp.CasesAverageConditional<DistributionStructArray<Bernoulli,bool>>(vBernoulli18, Cloudy_selector_cases_F);
			// The constant 'vBernoulli19'
			Bernoulli vBernoulli19 = Bernoulli.FromLogOdds(-2.1972245773362191);
			DistributionStructArray<Bernoulli,bool> Sprinkler_cond_Cloudy_F = default(DistributionStructArray<Bernoulli,bool>);
			// Create array for 'Sprinkler_cond_Cloudy' Forwards messages.
			Sprinkler_cond_Cloudy_F = new DistributionStructArray<Bernoulli,bool>(2);
			for(int _gateind = 0; _gateind<2; _gateind++) {
				Sprinkler_cond_Cloudy_F[_gateind] = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli19);
			}
			// Message to 'Sprinkler_cond_Cloudy' from Copy factor
			Sprinkler_cond_Cloudy_F[0] = Factor.Copy<Bernoulli>(vBernoulli19);
			// Message to 'Sprinkler_cond_Cloudy' from Copy factor
			Sprinkler_cond_Cloudy_F[1] = Factor.Copy<Bernoulli>(vBernoulli18);
			Bernoulli Rain_cond_Sprinkler_0_selector_cases_0_B = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
			// Message to 'Rain_cond_Sprinkler_0_selector_cases_0' from Random factor
			Rain_cond_Sprinkler_0_selector_cases_0_B = Bernoulli.FromLogOdds(UnaryOp<bool>.LogEvidenceRatio<Bernoulli>(this.wetGrass, vBernoulli23));
			DistributionStructArray<Bernoulli,bool> Rain_cond_Sprinkler_0_selector_cases_B = default(DistributionStructArray<Bernoulli,bool>);
			// Create array for 'Rain_cond_Sprinkler_0_selector_cases' Backwards messages.
			Rain_cond_Sprinkler_0_selector_cases_B = new DistributionStructArray<Bernoulli,bool>(2);
			for(int _ind0 = 0; _ind0<2; _ind0++) {
				Rain_cond_Sprinkler_0_selector_cases_B[_ind0] = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
			}
			// Message to 'Rain_cond_Sprinkler_0_selector_cases' from Copy factor
			Rain_cond_Sprinkler_0_selector_cases_B[0] = Factor.Copy<Bernoulli>(Rain_cond_Sprinkler_0_selector_cases_0_B);
			// The constant 'vBernoulli24'
			Bernoulli vBernoulli24 = Bernoulli.FromLogOdds(2.1972245773362196);
			Bernoulli Rain_cond_Sprinkler_0_selector_cases_1_B = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
			// Message to 'Rain_cond_Sprinkler_0_selector_cases_1' from Random factor
			Rain_cond_Sprinkler_0_selector_cases_1_B = Bernoulli.FromLogOdds(UnaryOp<bool>.LogEvidenceRatio<Bernoulli>(this.wetGrass, vBernoulli24));
			// Message to 'Rain_cond_Sprinkler_0_selector_cases' from Copy factor
			Rain_cond_Sprinkler_0_selector_cases_B[1] = Factor.Copy<Bernoulli>(Rain_cond_Sprinkler_0_selector_cases_1_B);
			Bernoulli[] Sprinkler_selector_cases_0_uses_B = default(Bernoulli[]);
			// Create array for 'Sprinkler_selector_cases_0_uses' Backwards messages.
			Sprinkler_selector_cases_0_uses_B = new Bernoulli[5];
			for(int _ind = 0; _ind<5; _ind++) {
				Sprinkler_selector_cases_0_uses_B[_ind] = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
			}
			Bernoulli Sprinkler_selector_cases_0_B = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
			DistributionStructArray<Bernoulli,bool> Sprinkler_selector_cases_B = default(DistributionStructArray<Bernoulli,bool>);
			// Create array for 'Sprinkler_selector_cases' Backwards messages.
			Sprinkler_selector_cases_B = new DistributionStructArray<Bernoulli,bool>(2);
			for(int _ind0 = 0; _ind0<2; _ind0++) {
				Sprinkler_selector_cases_B[_ind0] = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
			}
			Bernoulli Rain_cond_Sprinkler_1_selector_cases_0_B = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
			// Message to 'Rain_cond_Sprinkler_1_selector_cases_0' from Random factor
			Rain_cond_Sprinkler_1_selector_cases_0_B = Bernoulli.FromLogOdds(UnaryOp<bool>.LogEvidenceRatio<Bernoulli>(this.wetGrass, vBernoulli24));
			DistributionStructArray<Bernoulli,bool> Rain_cond_Sprinkler_1_selector_cases_B = default(DistributionStructArray<Bernoulli,bool>);
			// Create array for 'Rain_cond_Sprinkler_1_selector_cases' Backwards messages.
			Rain_cond_Sprinkler_1_selector_cases_B = new DistributionStructArray<Bernoulli,bool>(2);
			for(int _ind0 = 0; _ind0<2; _ind0++) {
				Rain_cond_Sprinkler_1_selector_cases_B[_ind0] = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
			}
			// Message to 'Rain_cond_Sprinkler_1_selector_cases' from Copy factor
			Rain_cond_Sprinkler_1_selector_cases_B[0] = Factor.Copy<Bernoulli>(Rain_cond_Sprinkler_1_selector_cases_0_B);
			// The constant 'vBernoulli26'
			Bernoulli vBernoulli26 = Bernoulli.FromLogOdds(-2.4423470353692043);
			Bernoulli Rain_cond_Sprinkler_1_selector_cases_1_B = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
			// Message to 'Rain_cond_Sprinkler_1_selector_cases_1' from Random factor
			Rain_cond_Sprinkler_1_selector_cases_1_B = Bernoulli.FromLogOdds(UnaryOp<bool>.LogEvidenceRatio<Bernoulli>(this.wetGrass, vBernoulli26));
			// Message to 'Rain_cond_Sprinkler_1_selector_cases' from Copy factor
			Rain_cond_Sprinkler_1_selector_cases_B[1] = Factor.Copy<Bernoulli>(Rain_cond_Sprinkler_1_selector_cases_1_B);
			Bernoulli[] Sprinkler_selector_cases_1_uses_B = default(Bernoulli[]);
			// Create array for 'Sprinkler_selector_cases_1_uses' Backwards messages.
			Sprinkler_selector_cases_1_uses_B = new Bernoulli[5];
			for(int _ind = 0; _ind<5; _ind++) {
				Sprinkler_selector_cases_1_uses_B[_ind] = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
			}
			Bernoulli Sprinkler_selector_cases_1_B = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
			Bernoulli[] Sprinkler_selector_uses_B = default(Bernoulli[]);
			// Create array for 'Sprinkler_selector_uses' Backwards messages.
			Sprinkler_selector_uses_B = new Bernoulli[2];
			for(int _ind = 0; _ind<2; _ind++) {
				Sprinkler_selector_uses_B[_ind] = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli19);
			}
			Bernoulli Sprinkler_selector_B = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli19);
			// Buffer for Replicate2BufferOp.UsesAverageConditional<DistributionStructArray<Bernoulli,bool>>
			DistributionStructArray<Bernoulli,bool> Cloudy_selector_cases_uses_B_marginal = default(DistributionStructArray<Bernoulli,bool>);
			// Message to 'Cloudy_selector_cases_uses' from Replicate factor
			Cloudy_selector_cases_uses_B_marginal = Replicate2BufferOp.MarginalInit<DistributionStructArray<Bernoulli,bool>>(Cloudy_selector_cases_F);
			DistributionStructArray<Bernoulli,bool>[] Cloudy_selector_cases_uses_F = default(DistributionStructArray<Bernoulli,bool>[]);
			// Create array for 'Cloudy_selector_cases_uses' Forwards messages.
			Cloudy_selector_cases_uses_F = new DistributionStructArray<Bernoulli,bool>[4];
			for(int _ind = 0; _ind<4; _ind++) {
				// Create array for 'Cloudy_selector_cases_uses' Forwards messages.
				Cloudy_selector_cases_uses_F[_ind] = new DistributionStructArray<Bernoulli,bool>(2);
				for(int _iv0 = 0; _iv0<2; _iv0++) {
					Cloudy_selector_cases_uses_F[_ind][_iv0] = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
				}
			}
			Bernoulli Sprinkler_F = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli19);
			// Buffer for Replicate2BufferOp.UsesAverageConditional<Bernoulli>
			Bernoulli Sprinkler_selector_uses_B_marginal = default(Bernoulli);
			// Message to 'Sprinkler_selector_uses' from Replicate factor
			Sprinkler_selector_uses_B_marginal = Replicate2BufferOp.MarginalInit<Bernoulli>(Sprinkler_F);
			Bernoulli[] Sprinkler_selector_uses_F = default(Bernoulli[]);
			// Create array for 'Sprinkler_selector_uses' Forwards messages.
			Sprinkler_selector_uses_F = new Bernoulli[2];
			for(int _ind = 0; _ind<2; _ind++) {
				Sprinkler_selector_uses_F[_ind] = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli19);
			}
			Bernoulli Rain_cond_Sprinkler_0_selector_B = ArrayHelper.MakeUniform<Bernoulli>(this.vBernoulli21);
			// Message to 'Rain_cond_Sprinkler_0_selector' from Cases factor
			Rain_cond_Sprinkler_0_selector_B = CasesOp.BAverageConditional(Rain_cond_Sprinkler_0_selector_cases_B);
			DistributionStructArray<Bernoulli,bool> Rain_cond_Sprinkler_B = default(DistributionStructArray<Bernoulli,bool>);
			// Create array for 'Rain_cond_Sprinkler' Backwards messages.
			Rain_cond_Sprinkler_B = new DistributionStructArray<Bernoulli,bool>(2);
			for(int _gateind = 0; _gateind<2; _gateind++) {
				Rain_cond_Sprinkler_B[_gateind] = ArrayHelper.MakeUniform<Bernoulli>(this.vBernoulli21);
			}
			// Message to 'Rain_cond_Sprinkler' from Copy factor
			Rain_cond_Sprinkler_B[0] = Factor.Copy<Bernoulli>(Rain_cond_Sprinkler_0_selector_B);
			Bernoulli Rain_cond_Sprinkler_1_selector_B = ArrayHelper.MakeUniform<Bernoulli>(this.vBernoulli21);
			// Message to 'Rain_cond_Sprinkler_1_selector' from Cases factor
			Rain_cond_Sprinkler_1_selector_B = CasesOp.BAverageConditional(Rain_cond_Sprinkler_1_selector_cases_B);
			// Message to 'Rain_cond_Sprinkler' from Copy factor
			Rain_cond_Sprinkler_B[1] = Factor.Copy<Bernoulli>(Rain_cond_Sprinkler_1_selector_B);
			for(int iteration = this.Changed_numberOfIterationsDecreased_WetGrass_iterationsDone; iteration<numberOfIterations; iteration++) {
				// Message to 'Cloudy_selector_cases_uses' from Replicate factor
				Cloudy_selector_cases_uses_B_marginal = Replicate2BufferOp.Marginal<DistributionStructArray<Bernoulli,bool>>(this.Cloudy_selector_cases_uses_B, Cloudy_selector_cases_F, Cloudy_selector_cases_uses_B_marginal);
				// Message to 'Cloudy_selector_cases_uses' from Replicate factor
				Cloudy_selector_cases_uses_F[1] = Replicate2BufferOp.UsesAverageConditional<DistributionStructArray<Bernoulli,bool>>(this.Cloudy_selector_cases_uses_B, Cloudy_selector_cases_F, Cloudy_selector_cases_uses_B_marginal, 1, Cloudy_selector_cases_uses_F[1]);
				// Message to 'Rain' from Exit factor
				Rain_F = GateExitOp<bool>.ExitAverageConditional<Bernoulli>(this.Rain_use_B, Cloudy_selector_cases_uses_F[1], Rain_cond_Cloudy_F, Rain_F);
				// Message to 'Sprinkler_selector_cases_1_uses' from Cases factor
				Sprinkler_selector_cases_1_uses_B[4] = Bernoulli.FromLogOdds(CasesOp.LogEvidenceRatio(Rain_cond_Sprinkler_1_selector_cases_B, Rain_F));
				// Message to 'Sprinkler_selector_cases_1' from Replicate factor
				Sprinkler_selector_cases_1_B = ReplicateOp.DefAverageConditional<Bernoulli>(Sprinkler_selector_cases_1_uses_B, Sprinkler_selector_cases_1_B);
				// Message to 'Sprinkler_selector_cases' from Copy factor
				Sprinkler_selector_cases_B[1] = Factor.Copy<Bernoulli>(Sprinkler_selector_cases_1_B);
				// Message to 'Sprinkler_selector_cases_0_uses' from Cases factor
				Sprinkler_selector_cases_0_uses_B[4] = Bernoulli.FromLogOdds(CasesOp.LogEvidenceRatio(Rain_cond_Sprinkler_0_selector_cases_B, Rain_F));
				// Message to 'Sprinkler_selector_cases_0' from Replicate factor
				Sprinkler_selector_cases_0_B = ReplicateOp.DefAverageConditional<Bernoulli>(Sprinkler_selector_cases_0_uses_B, Sprinkler_selector_cases_0_B);
				// Message to 'Sprinkler_selector_cases' from Copy factor
				Sprinkler_selector_cases_B[0] = Factor.Copy<Bernoulli>(Sprinkler_selector_cases_0_B);
				// Message to 'Sprinkler_selector_uses' from Cases factor
				Sprinkler_selector_uses_B[0] = CasesOp.BAverageConditional(Sprinkler_selector_cases_B);
				// Message to 'Sprinkler_selector' from Replicate factor
				Sprinkler_selector_B = ReplicateOp.DefAverageConditional<Bernoulli>(Sprinkler_selector_uses_B, Sprinkler_selector_B);
				// Message to 'Cloudy_selector_cases_uses' from Exit factor
				this.Cloudy_selector_cases_uses_B[3] = GateExitOp<bool>.CasesAverageConditional<Bernoulli,DistributionStructArray<Bernoulli,bool>>(Sprinkler_selector_B, Sprinkler_cond_Cloudy_F, this.Cloudy_selector_cases_uses_B[3]);
				// Message to 'Cloudy_selector_cases_uses' from Replicate factor
				Cloudy_selector_cases_uses_B_marginal = Replicate2BufferOp.Marginal<DistributionStructArray<Bernoulli,bool>>(this.Cloudy_selector_cases_uses_B, Cloudy_selector_cases_F, Cloudy_selector_cases_uses_B_marginal);
				// Message to 'Cloudy_selector_cases_uses' from Replicate factor
				Cloudy_selector_cases_uses_F[3] = Replicate2BufferOp.UsesAverageConditional<DistributionStructArray<Bernoulli,bool>>(this.Cloudy_selector_cases_uses_B, Cloudy_selector_cases_F, Cloudy_selector_cases_uses_B_marginal, 3, Cloudy_selector_cases_uses_F[3]);
				// Message to 'Sprinkler' from Exit factor
				Sprinkler_F = GateExitOp<bool>.ExitAverageConditional<Bernoulli>(Sprinkler_selector_B, Cloudy_selector_cases_uses_F[3], Sprinkler_cond_Cloudy_F, Sprinkler_F);
				// Message to 'Sprinkler_selector_uses' from Replicate factor
				Sprinkler_selector_uses_B_marginal = Replicate2BufferOp.Marginal<Bernoulli>(Sprinkler_selector_uses_B, Sprinkler_F, Sprinkler_selector_uses_B_marginal);
				// Message to 'Sprinkler_selector_uses' from Replicate factor
				Sprinkler_selector_uses_F[1] = Replicate2BufferOp.UsesAverageConditional<Bernoulli>(Sprinkler_selector_uses_B, Sprinkler_F, Sprinkler_selector_uses_B_marginal, 1, Sprinkler_selector_uses_F[1]);
				// Message to 'Rain_use' from EnterPartial factor
				this.Rain_use_B = GateEnterPartialOp<bool>.ValueAverageConditional<Bernoulli>(Rain_cond_Sprinkler_B, Sprinkler_selector_uses_F[1], Rain_F, new int[2] {0,1}, this.Rain_use_B);
				// Message to 'Cloudy_selector_cases_uses' from Exit factor
				this.Cloudy_selector_cases_uses_B[1] = GateExitOp<bool>.CasesAverageConditional<Bernoulli,DistributionStructArray<Bernoulli,bool>>(this.Rain_use_B, Rain_cond_Cloudy_F, this.Cloudy_selector_cases_uses_B[1]);
				this.OnProgressChanged(new ProgressChangedEventArgs(iteration));
			}
			DistributionStructArray<Bernoulli,bool> Cloudy_selector_cases_B = default(DistributionStructArray<Bernoulli,bool>);
			// Create array for 'Cloudy_selector_cases' Backwards messages.
			Cloudy_selector_cases_B = new DistributionStructArray<Bernoulli,bool>(2);
			for(int _iv0 = 0; _iv0<2; _iv0++) {
				Cloudy_selector_cases_B[_iv0] = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
			}
			// Message to 'Cloudy_selector_cases' from Replicate factor
			Cloudy_selector_cases_B = ReplicateOp.DefAverageConditional<DistributionStructArray<Bernoulli,bool>>(this.Cloudy_selector_cases_uses_B, Cloudy_selector_cases_B);
			Bernoulli Cloudy_selector_B = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli18);
			// Message to 'Cloudy_selector' from Cases factor
			Cloudy_selector_B = CasesOp.BAverageConditional(Cloudy_selector_cases_B);
			// Message to 'Cloudy_marginal' from Variable factor
			this.Cloudy_marginal_F = VariableOp.MarginalAverageConditional<Bernoulli>(Cloudy_selector_B, vBernoulli18, this.Cloudy_marginal_F);
			this.Rain_marginal_F = ArrayHelper.MakeUniform<Bernoulli>(this.vBernoulli21);
			// Message to 'Rain_marginal' from DerivedVariable factor
			this.Rain_marginal_F = DerivedVariableOp.MarginalAverageConditional<Bernoulli>(this.Rain_use_B, Rain_F, this.Rain_marginal_F);
			this.Sprinkler_marginal_F = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli19);
			// Message to 'Sprinkler_marginal' from DerivedVariable factor
			this.Sprinkler_marginal_F = DerivedVariableOp.MarginalAverageConditional<Bernoulli>(Sprinkler_selector_B, Sprinkler_F, this.Sprinkler_marginal_F);
			this.Changed_numberOfIterationsDecreased_WetGrass_iterationsDone = numberOfIterations;
		}

		/// <summary>
		/// Returns the marginal distribution for 'Cloudy' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public Bernoulli CloudyMarginal()
		{
			return this.Cloudy_marginal_F;
		}

		/// <summary>
		/// Returns the marginal distribution for 'Rain' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public Bernoulli RainMarginal()
		{
			return this.Rain_marginal_F;
		}

		/// <summary>
		/// Returns the marginal distribution for 'Sprinkler' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public Bernoulli SprinklerMarginal()
		{
			return this.Sprinkler_marginal_F;
		}

		/// <summary>
		/// Returns the marginal distribution for 'WetGrass' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public Bernoulli WetGrassMarginal()
		{
			return this.WetGrass_marginal;
		}

		#endregion

		#region Events
		/// <summary>Event that is fired when the progress of inference changes, typically at the end of one iteration of the inference algorithm.</summary>
		public event EventHandler<ProgressChangedEventArgs> ProgressChanged;
		#endregion

	}

}
